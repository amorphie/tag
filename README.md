# Amorphie.tag

Simple tag management service for developers.

Some features are;
* Tag definition repository with tag grouping functionality (Yes, tagging the tags :))
* Tags can have enrichment url to enrich tagged entity item. 
* Tags can generate views for tagged entity item.
* Simple project wide repository for domain and entity records.


## Prerequisites

### Install Dotnet 7.0
https://dotnet.microsoft.com/en-us/download/dotnet/7.0

### Install Dapr
https://docs.dapr.io/getting-started/install-dapr-cli/

> **Warning**
> Init dapr in slim mode. If any dapr component is running on docker, delete them.

```
dapr init --slim
```

### Mocking 
Use https://www.mockoon.com to design mocks.
Mock definitions ([mocks.json](https://github.com/amorphie/tag/blob/main/mocks.json)) are stored workspace root folder.

You can run mocks directly on mockoon desktop application or docker image.
To start docker pod just run the **(Mock) Start Mock Services from Docker** task from [Task Explorer (vscode add-on)](https://marketplace.visualstudio.com/items?itemName=spmeesseman.vscode-taskexplorer)

### Automated Test

Project is focused to API testing for validating the functionality.

Project uses https://www.thunderclient.com (vscode add-on) as a testing tool.

All test collections are stored in (thunder-tests)[https://github.com/amorphie/tag/blob/main/thunder-tests] folder.


> **Warning**
> Make required configuration to use shared test collection in Thunder Client. 
> https://github.com/rangav/thunder-client-support#team


### Setup Environment (Task)

A existing task make your development environment ready.

Project includes [docker compose file](https://github.com/amorphie/tag/blob/main/docker-compose.yml) in root folder. The *Setup Environment* task just executes it.

```
docker-compose up -d
```

The compose file is installs 
* Redis *(with blank password)*
* Redis Insight for Redis management
* Postgres *(user:posgres, password:posgres)*
* PG Admin for Posgres management
* Dapr placement server
* Prometheus as a metric server

Redis Insight: http://localhost:5501
PGAdmin: http://localhost:5502

## Project

Then just press F5

Project endpoints|

* Tag Swagger URL: http://localhost:4101/swagger/index.html
* Tag Dapr endpoint: http://localhost:41010/v1.0/invoke/amorphie-tag/method/tag
* Tag.Execute Swagger URL: http://localhost:4102/swagger/index.html
* Tag.Execute Dapr endpoint: http://localhost:41020/v1.0/invoke/amorphie-tag-execute/method/tag/{tag-name}/execute?{parameters}

### Project Notes
* Dapr components and configuration files are stored in root [dapr](https://github.com/amorphie/tag/blob/main/dapr) folder.
* Applications are starts as dapr sidecar (daprd).
* Application data module is EF based separate [project](https://github.com/amorphie/tag/blob/main/amorphie.tag.data).