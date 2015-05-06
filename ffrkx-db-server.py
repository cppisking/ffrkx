import argparse
import sys
from ffrkx.db_server import database
from ffrkx.db_server import requestserver, testserver

try:
    parser = argparse.ArgumentParser("FFRKX Database Server")
    parser.add_argument("--mode", choices=['test', 'server'])
    parser.add_argument("--port", type=int, default=50007)
    parser.add_argument("--user", "-u", required=True)
    parser.add_argument("--password", "-p", required=True)
    parser.add_argument("--host", "-H", required=True)
    parser.add_argument("--database", "-d", required=True)
    args = parser.parse_args()

    db = database.Database(args.user, args.password, args.host, args.database)
    if args.mode == 'test':
        server = testserver.DBTestServer(db)
        server.run()
    elif args.mode == 'server':
        server = requestserver.DBRequestServer(args.port, db)
        server.run()
except KeyboardInterrupt:
    print "Ctrl+C received, shutting down..."
