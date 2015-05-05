import json

def decode(string):
    """Windows likely does not have utf8 as the system encoding and Python is
    too stubborn to provide a sensible default."""
    return json.loads(string.decode('utf-8'))

def dump(data):
    print json.dumps(data, sort_keys=True, indent=2, separators=(',', ': '))
