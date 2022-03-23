FROM python

MAINTAINER LaFaCa+Co "softwareengineering@sowiho.de"

#copy just the requirements.txt first to leverage Docker cache
COPY ./requirements.txt /app/requirements.txt

ENV FLASK_APP=app

WORKDIR /app

COPY . /app

VOLUME ./data

RUN pip install -r requirements.txt

ENTRYPOINT [ "python" ]

CMD [ "app.py" ]
