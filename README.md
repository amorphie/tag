# Amorphie.tag

## Prerequisites

### Install Dotnet 7.0
https://dotnet.microsoft.com/en-us/download/dotnet/7.0

###  Install Dapr
https://docs.dapr.io/getting-started/install-dapr-cli/

### Test Tool
https://mockoon.com

### Automated Test 

Postman Collections

> **Warning**
> Init dapr in slim mode. If any dapr component is running on docker, delete them.

```
dapr init --slim
```
### Prepare Development Enviroment

```
docker-compose up -d
```


The compose file is installs 
* Redis *(with blank password)*
* Redis Insight for Redis management
* Postgres *(user:posgres, password:posgres)*
* PG Admin for Posgres management
* Dapr placement server

Redis Insight : http://localhost:5501
PGAdmin : http://localhost:5502

## Project

Then just press F5

Project endpoints

* Tag Swagger URL: http://localhost:4101/swagger/index.html
* Dapr endpoint: http://localhost:41010/v1.0/invoke/amorphie-tag/method/tag

* Tag.Execute Swagger URL: http://localhost:4102/swagger/index.html
* Dapr endpoint: http://localhost:41020/v1.0/invoke/amorphie-tag-execute/method/tag/{tag-name}/execute?{parameters}

