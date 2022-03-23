@app.route('/')
#returns text

@app.route('/test')
# returns text

@app.route('/getcablefilelist')
# returns json object containing list of cables

@app.route('/getcable')
# pass cable name in parameter "cablename"
# curl -XGET "localhost:5000/getcable?cablename=cable_caex_class_model.xml"

@app.route('/writecable', methods=["POST"])
# Using the POST method, the JSON in the body is written to a file specified using the argument "cablename"
# curl -XPOST -H "Content-Type: application/json" -d '{"name": "linuxize", "email": "linuxize@example.com"}' "localhost:5000/writecable?cablename=cable_test.xml"