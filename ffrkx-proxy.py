import os
import socket
import sys
import traceback

from ffrkx.proxy.proxy import FFRKXProxy

from libmproxy import proxy
from libmproxy.proxy.server import ProxyServer

try:
    config = proxy.ProxyConfig(port=8080)
    host = ProxyServer(config)

    print "Configure your phone's proxy to point to this computer, then visit mitm.it"
    print "on your phone to install the interception certificate.\n"
    print "Record Peeker is listening on {0}:{1}, on these addresses:".format(host.address.host, host.address.port)
    print "Try entering the Party screen, or starting a battle."

    m = FFRKXProxy(host)
    m.run()
except:
    print traceback.format_exc()
    sys.exit(0)
