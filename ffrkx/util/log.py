import datetime
import sys

def log_message(str):
    today = datetime.datetime.today()
    timestamp = today.strftime("%H:%M:%S.%f")
    print "[{0}] {1}".format(timestamp, str)
    pass

def log_exception(message):
    exc_type = sys.exc_info()[0]
    exception = sys.exc_info()[1]
    log_message("Exception '{0}' encountered: {1} (Args = {2})".format(exc_type.__name__, message, exception.args))
    pass
