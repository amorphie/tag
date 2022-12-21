# Amorphie.tag

## Prerequisites

### Install Dotnet 7.0
https://dotnet.microsoft.com/en-us/download/dotnet/7.0

###  Install Dapr
https://docs.dapr.io/getting-started/install-dapr-cli/

**Warning**
Init dapr in slim mode
```
dapr init --slim
```

## Project

Prepare dapr
```
dapr init --slim
```

Prepare development enviroment
```
docker-compose up -d
```

Then just press F5






## OBSLOTE 

To build database run;

> dotnet ef database update

## Amorphie.tag
Tag repository for general use. 

To Run
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path Components dotnet run -- urls=http://localhost:4001/
```

### Endpoints 
* Swagger URL: http://localhost:4001/swagger/index.html
* Dapr endpoint: http://localhost:40001/v1.0/invoke/amorphie-tag/method/tag


## Amorphie.tag.execute

Tag related enricchment service

To Run
```
dapr run --app-id amorphie-tag-execute  --app-port 4002  --dapr-http-port 40002 --components-path Components dotnet run -- urls=http://localhost:4002/
```

### Endpoints 
* Swagger URL: http://localhost:4002/swagger/index.html
* Dapr endpoint: http://localhost:40002/v1.0/invoke/amorphie-tag-execute/method/tag/{tag-name}/execute?{parameters}

