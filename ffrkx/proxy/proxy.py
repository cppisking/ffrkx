import errno
import json
import re
import socket
import struct
from threading import Event, Thread
import traceback

from libmproxy import controller
from libmproxy.protocol.http import decoded, HTTPResponse

import ffrkx.proto.messages_pb2 as ffrkx_proto

class FFRKXProxy(controller.Master):
    __default_handler = None
    __handlers = []

    @staticmethod
    def register_default_handler(handler):
        FFRKXProxy.__default_handler = handler

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
            print "Connecting to db server on localhost:50007"
            while True:
                if self._shutdown_event.is_set():
                    return

                try:
                    self._db_socket = socket.create_connection(('localhost', 50007))
                    print "Connected to db server, setting event..."
                    self._db_connected_event.set()
                    return
                except socket.error as err:
                   # ECONNREFUSED means nobody is listening, and ETIMEDOUT means it timed out.
                   # In either case, keep trying until we get a connection or we try to shutdown.
                    if (err.errno != errno.ECONNREFUSED) and (err.errno != errno.ETIMEDOUT):
                        raise
        except Exception as err:
            print "An unknown error occurred connecting to localhost:50007.  %s" % err.message

    def run(self):
        try:
            print "Running handler..."
            result = controller.Master.run(self)
            print "Returning from handler..."
            return result
        except:
            print "Exception received, shutting down..."
            self.shutdown()
            raise

    def handle_request(self, flow):
        flow.reply()

    def send_to_db_server(self, message):
        data = message.SerializeToString()
        if self._db_connected_event.is_set():
            print "About to send %s byte message" % len(data)
            self._db_socket.sendall(struct.pack("I", len(data)))
            self._db_socket.sendall(data)

    def handle_response(self, flow):
        #if not self._db_connected_event.is_set():
        #    print "The event is not set, returning..."
        #    flow.reply()
        #    return
        if not flow.request.pretty_host(hostheader=True).endswith('ffrk.denagames.com'):
            print "Received non-FFRK event, returning..."
            flow.reply()
            return
        try:
            print flow.request.path
            handler = next((x[1] for x in FFRKXProxy.__handlers if x[0] in flow.request.path), None)
            if handler != None:
                with decoded(flow.response):
                    data = json.loads(unicode(flow.response.content, "utf-8"))
                    handler(self, flow.request.path, data)
        except KeyboardInterrupt:
            raise
        except Exception as err:
            print "An error occurred sending information to the db socket (%s).  Closing connection..." % err.message
            traceback.print_exc()
            self.spawn_db_listener_thread()
        flow.reply()
