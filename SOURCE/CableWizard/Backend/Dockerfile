FROM ubuntu:20.04

MAINTAINER LaFaCa+Co "softwareengineering@sowiho.de"

RUN apt-get update -y && \
    apt-get install -y python3-pip python-dev

#copy just the requirements.txt first to leverage Docker cache
COPY ./requirements.txt /app/requirements.txt

WORKDIR /app

RUN pip install -r requirements.txt

COPY . /app

VOLUME ./data/

ENTRYPOINT [ "python" ]

CMD [ "app.py" ]
