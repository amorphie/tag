# Prerequisites

Install Dapr : https://docs.dapr.io/getting-started/install-dapr-cli/

Plus, for **Queryable State Store** install MongoDB, *optionally MongoDB Compass*

```
docker run -d --rm -p 27017:27017 --name mongodb mongo:5
```

## Amorphie.tag

Direct
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path /Components dotnet run 
```

Hot reload
```
dapr run --app-id amorphie-tag  --app-port 4001  --dapr-http-port 40001 --components-path /Components dotnet watch
```