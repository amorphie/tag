# Amorphie.tag

## Prerequisites

### Install Dotnet 7.0
https://dotnet.microsoft.com/en-us/download/dotnet/7.0

*if you are using RC-2 add below enviroment variable*
>export DOTNET_CLI_DO_NOT_USE_MSBUILD_SERVER=1

###  Install Dapr
https://docs.dapr.io/getting-started/install-dapr-cli/

### Install Postgres

```
docker run -e POSTGRES_PASSWORD=example -d postgres
```
### Set configurations

With Redis CLI save below configruration items.
> MSET config-amorphie-tag-db "Host=localhost:5432;Database=tags;Username=postgres;Password=example||1" 

### Mockoon 

Install Mockoon and load mocks https://github.com/amorphie/tag/blob/main/tests/mocks/mocks.json

## Amorphie.tag.data

EF Core based persistancy module. Also includes common poco models.

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

