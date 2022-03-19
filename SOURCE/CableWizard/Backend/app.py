from os.path import isfile, join

from flask import Flask, request
from os import listdir
import json
import xmltodict

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


#cable files may only start with "cable"
@app.route('/getcablefilelist')
def get_cable_file_list():
    mypath = "./data/cables"
    onlyfiles = [f for f in listdir(mypath) if isfile(join(mypath, f))]
    onlycables = [x for x in onlyfiles if (x[:5] == "cable")]
    return json.dumps(onlycables)


#pass cable name in parameter "cablename"
#curl -XGET "localhost:5000/getcable?cablename=cable_caex_class_model.xml"
@app.route('/getcable')
def get_cable():
    cablename = request.args.get('cablename')
    print("get_cable() You requested cable " + cablename)
    cable_file_path = "./data/cables/" + cablename

    file = open(cable_file_path, 'r')
    content = file.read()

    o = xmltodict.parse(content)
    jsonxml = json.dumps(o)

    return jsonxml


if __name__ == '__main__':
    app.run()
