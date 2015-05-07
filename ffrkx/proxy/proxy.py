import errno
import json
import re
import socket
import struct
from threading import Event, Thread
import traceback

from libmproxy import controller
from libmproxy.protocol.http import decoded, HTTPResponse

from ffrkx.proto import messages_pb2
from ffrkx.util import log

class FFRKXProxy(controller.Master):
    __handlers = []

    @staticmethod
    def register_handler(path, handler):
        FFRKXProxy.__handlers.append((path, handler))

    def __init__(self, server):
        controller.Master.__init__(self, server)

        self._shutdown_event = Event()
        self._db_connected_event = Event()

        self.spawn_db_listener_thread()

    def join_listener_thread(self):
        if self._listener_thread:
            self._listener_thread.join()
        self._listener_thread = None

    def shutdown(self):
        self._shutdown_event.set()
        self.join_listener_thread()
        controller.Master.shutdown(self)

    def spawn_db_listener_thread(self):
        self._db_connected_event.clear()
        self._db_socket = None
        self._listener_thread = Thread(target = self.connect_to_db_server)
        self._listener_thread.start()

    def connect_to_db_server(self):
        try:
            log.log_message("Connecting to db server on localhost:50007")
            while True:
                if self._shutdown_event.is_set():
                    return

                try:
                    self._db_socket = socket.create_connection(('localhost', 50007))
                    log.log_message("Connected to db server, setting event...")
                    self._db_connected_event.set()
                    return
                except socket.error as err:
                   # ECONNREFUSED means nobody is listening, and ETIMEDOUT means it timed out.
                   # In either case, keep trying until we get a connection or we try to shutdown.
                    if (err.errno != errno.ECONNREFUSED) and (err.errno != errno.ETIMEDOUT):
                        raise
        except Exception as err:
            log.log_exception("An unknown error occurred connecting to localhost:50007.")

    def run(self):
        try:
            result = controller.Master.run(self)
            return result
        except:
            log.log_exception("Exception received while running proxy server, shutting down...")
            self.shutdown()
            raise

    def handle_request(self, flow):
        flow.reply()

    def send_to_db_server(self, message):
        assert(isinstance(message, messages_pb2.FFRKProxyMessage))
        data = message.SerializeToString()
        if self._db_connected_event.is_set():
            log.log_message("Sending %s byte message to database server" % len(data))
            self._db_socket.sendall(struct.pack("I", len(data)))
            self._db_socket.sendall(data)

    def send_battle_encounter(self, message):
        assert(isinstance(message, messages_pb2.BattleEncounterMsg))
        proxy_msg = messages_pb2.FFRKProxyMessage()
        proxy_msg.battle_encounter.CopyFrom(message)
        self.send_to_db_server(proxy_msg)

    def send_list_dungeons(self, message):
        assert(isinstance(message, messages_pb2.ListDungeonsMsg))
        proxy_msg = messages_pb2.FFRKProxyMessage()
        proxy_msg.list_dungeons.CopyFrom(message)
        self.send_to_db_server(proxy_msg)

    def send_list_battles(self, message):
        assert(isinstance(message, messages_pb2.ListBattlesMsg))
        proxy_msg = messages_pb2.FFRKProxyMessage()
        proxy_msg.list_battles.CopyFrom(message)
        self.send_to_db_server(proxy_msg)

    def handle_response(self, flow):
        host = flow.request.pretty_host(hostheader=True)
        if not host.endswith('ffrk.denagames.com'):
            flow.reply()
            return
        try:
            log.log_message("FFRK Request: %s" % flow.request.path)
            handler = next((x[1] for x in FFRKXProxy.__handlers if x[0] in flow.request.path), None)
            if handler != None:
                with decoded(flow.response):
                    data = json.loads(unicode(flow.response.content, "utf-8"))
                    handler(self, flow.request.path, data)
        except KeyboardInterrupt:
            raise
        except Exception as err:
            log.log_exception("An error occurred sending information to the db socket, closing connection.")
            traceback.print_exc()
            self.spawn_db_listener_thread()
        flow.reply()
