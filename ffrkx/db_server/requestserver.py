import errno
import socket
import struct
import sys
import traceback

import ffrkx.db_server as db_server
import ffrkx.proto.messages_pb2 as ffrkx_proto
from ffrkx.util import log

class DBRequestServer:
    def __init__(self, port, db):
        self.db = db
        self.port = port

    def recv_exactly(self, sock, length):
        chunks = []
        bytes_received = 0
        while True:
            try:
                while bytes_received < length:
                    chunk = sock.recv(min(length - bytes_received, 2048))
                    chunk_len = len(chunk)
                    log.log_message("Received %s byte chunk" % chunk_len)
                    if chunk == '':
                        raise RuntimeError("socket connection broken")
                    chunks.append(chunk)
                    bytes_received += chunk_len
                return ''.join(chunks)
            except socket.error as err:
                pass

    def wait_for_connection(self):
        proxy_listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        proxy_listener.bind(('', self.port))
        proxy_listener.listen(5)
        proxy_listener.settimeout(1)
        log.log_message("Waiting for connection from proxy server...")
        while True:
            try:
                conn, addr = proxy_listener.accept()
                return conn
            except KeyboardInterrupt:
                raise
            except socket.error:
                pass

    def message_loop(self, sock):
        log.log_message("Connected to proxy server, entering message loop...")
        sock.settimeout(5)
        while True:
            try:
                dataLen = struct.unpack("I", self.recv_exactly(sock, 4))[0]
                log.log_message("Unpacked message length.  Expecting %s byte message" % dataLen)
                data = self.recv_exactly(sock, dataLen)
                log.log_message("received %s bytes of data" % len(data))
                message = ffrkx_proto.FFRKProxyMessage()
                message.ParseFromString(data)
                self.db.handle_message(message)
                pass
            except KeyboardInterrupt:
                raise
            except:
                log.log_exception("An error occurred on the socket.  Attempting to reconnect...")
                traceback.print_exc()
                sock.close()
                return

    def run(self):
        while True:
            sock = self.wait_for_connection()
            self.message_loop(sock)
