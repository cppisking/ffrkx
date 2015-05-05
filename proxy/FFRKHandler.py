import errno
import json
import re
import socket
import struct
from threading import Event, Thread

from libmproxy import controller
from libmproxy.protocol.http import decoded, HTTPResponse

import messages_pb2

class Handler(controller.Master):
    def __init__(self, server):
        controller.Master.__init__(self, server)

        self._db_socket = None
        self._db_connected_event = Event()

        self._listener_thread = Thread(target = self.connect_to_db_server)
        self._listener_thread.start()

    def join_listener_thread(self):
        if self._listener_thread:
            self._listener_thread.join()
        self._listener_thread = None

    def connect_to_db_server(self):
        try:
            print "Connecting to db server on 127.0.0.1:50007"
            self._db_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            while True:
                try:
                    self._db_socket.connect(('localhost', 50007))
                except socket.error as err:
                    if type(err.args) != tuple or err[0] != errno.ECONNREFUSED:
                        raise
                else:
                    print "Connected to db server, setting event..."
                    self._db_connected_event.set()
                    return
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
            join_listener_thread(self)

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
                message = messages_pb2.FFRKResponse()
                message.request_path = flow.request.path
                message.content = unicode(flow.response.content, "utf-8")
                data = message.SerializeToString()
                print "About to send %s byte message" % len(data)
                self._db_socket.sendall(struct.pack("I", len(data)))
                self._db_socket.sendall(data)
        except socket.error as err:
            print "An error occurred sending information to the db socket (%s).  Closing db thread..." % err.message
            self._db_connected_event.clear()
            self._db_socket = None
            listener_thread = Thread(target = self.listen_for_db_connection)
            listener_thread.start()
        print "Finished handling request, returning..."
        flow.reply()
