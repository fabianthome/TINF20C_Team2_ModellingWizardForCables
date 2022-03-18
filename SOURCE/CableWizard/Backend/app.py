from os.path import isfile, join

from flask import Flask, redirect
from os import listdir
import json

app = Flask(__name__)


@app.route('/')
def running():
    return "It's running!"


@app.route('/test')
def testing():
    return "It's reachable!"


# Function to convert
def list_to_string(s):
    # initialize an empty string
    str1 = ""

    # traverse in the string
    for ele in s:
        str1 += ele + "\n"

        # return string
    return str1


# cable files may only start with "cable"
@app.route('/getcablefilelist')
def get_cable_file_list():
    mypath = "./data/cables"
    onlyfiles = [f for f in listdir(mypath) if isfile(join(mypath, f))]
    onlycables = [x for x in onlyfiles if (x[:5] == "cable")]
    return json.dumps(onlycables)


if __name__ == '__main__':
    app.run()
