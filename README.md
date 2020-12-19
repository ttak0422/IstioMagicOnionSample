# IstioMagicOnionSample

## BuildAll

```
$ ./build.sh
```

## BuildDockerImage

```
# Server
$ ./build.sh --target=BuildServerDocker
```

## Run

```
# Client
$ dotnet run -p SampleApp.Client/SampleApp.Client.csproj

# Server
$ dotnet run -p SampleApp.Server/SampleApp.Server.csproj
```