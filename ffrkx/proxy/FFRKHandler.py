import errno
import json
import re
import socket
import struct
from threading import Event, Thread

from libmproxy import controller
from libmproxy.protocol.http import decoded, HTTPResponse

import ffrkx.proto.messages_pb2 as ffrkx_proto

class Handler(controller.Master):
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

    def handle_response(self, flow):
        if not self._db_connected_event.is_set():
            print "The event is not set, returning..."
            flow.reply()
            return
        if not flow.request.pretty_host(hostheader=True).endswith('ffrk.denagames.com'):
            print "Received non-FFRK event, returning..."
            flow.reply()
            return
        print "Received FFRK event, processing..."
        try:
            with decoded(flow.response):
                message = ffrkx_proto.FFRKResponse()
                message.request_path = flow.request.path
                message.content = unicode(flow.response.content, "utf-8")
                data = message.SerializeToString()
                print "About to send %s byte message" % len(data)
                self._db_socket.sendall(struct.pack("I", len(data)))
                self._db_socket.sendall(data)
        except KeyboardInterrupt:
            raise
        except Exception as err:
            print "An error occurred sending information to the db socket (%s).  Closing connection..." % err.message
            self.spawn_db_listener_thread()
        flow.reply()
