import argparse

opts = None

def parse_args(argv):
    global opts
    parser = argparse.ArgumentParser("Test")
    parser.add_argument("--port", "-p", type=int, default=8080, help="Specify the port recordpeeker runs on")
    parser.add_argument("--verbosity", "-v", default=0, type=int, choices=[0,1,2,3], help="Spews more info. 1: prints the path of each request. 2: prints the content of unknown requests. 3: Also print the content of known requests.")
    opts = parser.parse_args(argv[1:])
