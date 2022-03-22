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


# cable files may only start with "cable"
@app.route('/getcablefilelist')
def get_cable_file_list():
    mypath = "./data/cables"
    print("get_cable_file_list(): Requested list of cable files in" + mypath)
    onlyfiles = [f for f in listdir(mypath) if isfile(join(mypath, f))]
    print("get_cable_file_list(): List of files in " + mypath + " is " + list_to_string(onlyfiles))
    onlycables = [x for x in onlyfiles if (x[:5] == "cable")]
    return json.dumps(onlycables)


# pass cable name in parameter "cablename"
# curl -XGET "localhost:5000/getcable?cablename=cable_caex_class_model.xml"
@app.route('/getcable')
def get_cable():
    cablename = request.args.get('cablename')
    print("get_cable(): You requested " + cablename)
    cable_file_path = "./data/cables/" + cablename

    file = open(cable_file_path, 'r')
    content = file.read()

    o = xmltodict.parse(content)
    jsonxml = json.dumps(o)

    return jsonxml


# Using the POST method, the JSON in the body is written to a file specified using the argument "cablename"
# curl -XPOST -H "Content-Type: application/json" -d '{"name": "linuxize", "email": "linuxize@example.com"}' "localhost:5000/writecable?cablename=cable_test.xml"
@app.route('/writecable', methods=["POST"])
def write_cable():
    cablename = request.args.get("cablename")
    print("write_cable(): You are trying to write the file \"" + cablename + "\" to disk.")

    if cablename[:6] != "cable_":
        print("write_cable(): ERROR! Cable name did not start with \"cable\"")
        return "406 Not Acceptable; Cable name did not start with \"cable_\"", 406
    elif cablename[-3:] != "xml":
        print("write_cable(): ERROR! Cable name did not have the proper extension")
        return "406 Not Acceptable; Cable name did not have the proper extension", 406

    cable_file_path = "./data/cables/" + cablename
    file = open(cable_file_path, 'w')
    request_data = request.json
    jsontoxml = xmltodict.unparse(request_data, pretty=True)
    file.write(jsontoxml)
    return "File written.", 200



if __name__ == '__main__':
    app.run()
