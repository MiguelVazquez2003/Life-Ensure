FROM python:3.11-slim

RUN apt-get -y update && apt-get -y install gcc git

COPY Pipfile /app/Pipfile
COPY Pipfile.lock /app/Pipfile.lock

WORKDIR /app

RUN pip install pipenv
RUN pipenv sync --bare --system

COPY . /app

