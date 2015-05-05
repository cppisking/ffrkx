import socket
import struct

import messages_pb2

def recv_exactly(sock, length):
    chunks = []
    bytes_received = 0
    try:
        while bytes_received < length:
            chunk = sock.recv(min(length - bytes_received, 2048))
            chunk_len = len(chunk)
            print "Received %s byte chunk" % chunk_len
            if chunk == '':
                print "Raising runtime error, socket is broken"
                raise RuntimeError("socket connection broken")
            chunks.append(chunk)
            bytes_received += chunk_len
        return ''.join(chunks)
    except Exception as err:
        print "An exception occurred while reading the socket. %s" % err.message

def wait_for_connection():
    proxy_listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    proxy_listener.bind(('', 50007))
    proxy_listener.listen(5)
    print "Waiting for connection from proxy server..."
    conn, addr = proxy_listener.accept()
    return conn

def message_loop(sock):
    while True:
        print "Connected to proxy server, waiting for message..."
        try:
            dataLen = struct.unpack("I", recv_exactly(sock, 4))[0]
            print "Received 4 byte length.  Expecting %s byte message" % dataLen
            data = recv_exactly(sock, dataLen)
            if data == None:
                print "Data is None, what?"
            else:
                print "received %s bytes of data" % len(data)
            message = messages_pb2.FFRKResponse()
            message.ParseFromString(data)
            print message.request_path
            pass
        except socket.error as err:
            print "An error occurred on the socket.  Closing socket..."
            sock.close()
            return

try:
    while True:
        sock = wait_for_connection()
        message_loop(sock)
except KeyboardInterrupt:
    print "Ctrl+C received, shutting down..."