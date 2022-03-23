FROM python:3.9

WORKDIR /code/modellingWizardBackend

COPY ./requirements.txt /code/modellingWizardBackend/requirements.txt

RUN pip install --no-cache-dir --upgrade -r /code/modellingWizardBackend/requirements.txt

COPY ./ /code/modellingWizardBackend/

CMD ["gunicorn", "--conf", "gunicorn_conf.py", "--bind", "0.0.0.0:80", "wsgi:app"]