import os
import socket
import sys
import traceback

import FFRKHandler
import options

from libmproxy import proxy
from libmproxy.proxy.server import ProxyServer

try:
    # This is just here so that --help returns the arguments
    options.parse_args(sys.argv)

    config = proxy.ProxyConfig(port=options.opts.port)
    host = ProxyServer(config)

    print "Configure your phone's proxy to point to this computer, then visit mitm.it"
    print "on your phone to install the interception certificate.\n"
    print "Record Peeker is listening on {0}:{1}, on these addresses:".format(host.address.host, host.address.port)
    print "Try entering the Party screen, or starting a battle."

    m = FFRKHandler.Handler(host)
    m.run()
except:
    print "Got an exception!"
    print traceback.format_exc()
    print "Just printed the exception"
    sys.exit(0)
