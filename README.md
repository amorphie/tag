# Amorphie.tag

## Prerequisites

### Install Dotnet 7.0 : https://dotnet.microsoft.com/en-us/download/dotnet/7.0

*if you are using RC-2 add below enviroment variable*
>export DOTNET_CLI_DO_NOT_USE_MSBUILD_SERVER=1

###  Install Dapr : https://docs.dapr.io/getting-started/install-dapr-cli/
### For **Queryable State Store** install MongoDB, *optionally MongoDB Compass*

```
docker run -d --rm -p 27017:27017 --name mongodb mongo:5
```
### Set configurations

With Redis CLI save below configruration items.

> MSET config-amorphie-ss-tag "ss-tag||1" 

## Amorphie.tag
Tag repository for general use. 


###  Direct
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path Components dotnet run -- urls=http://localhost:4001/
```

###  Hot reload
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path Components dotnet watch -- urls=http://localhost:4001/
```

### Endpoints 
* Swagger URL: http://localhost:4001/swagger/index.html
* Dapr endpoint: http://localhost:40001/v1.0/invoke/amorphie-tag/method/tag
* Test calls: https://github.com/amorphie/tag/blob/main/amorphie.tag/test/rest/amorphie.tag.http