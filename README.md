# Amorphie.tag

## Prerequisites

Install Dotnet 7.0 : https://dotnet.microsoft.com/en-us/download/dotnet/7.0


*if you are using RC-2 add below enviroment variable*
>export DOTNET_CLI_DO_NOT_USE_MSBUILD_SERVER=1

Install Dapr : https://docs.dapr.io/getting-started/install-dapr-cli/

Plus, for **Queryable State Store** install MongoDB, *optionally MongoDB Compass*

```
docker run -d --rm -p 27017:27017 --name mongodb mongo:5
```

## Amorphie.tag

Direct
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path Components dotnet run -- urls=http://localhost:4001/
```

Hot reload
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path Components dotnet watch -- urls=http://localhost:4001/
```


Swagger URL: http://localhost:4001/swagger/index.html
Ddapr endpoint: http://localhost:40001/v1.0/invoke/amorphie-tag/method/tag