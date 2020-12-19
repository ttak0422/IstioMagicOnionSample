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

## K8S

```
# Create namespace
$ kubectl create ns sample-ns

# Label istio-injection
$ kubectl label namespaces sample-ns istio-injection=enabled

# Apply IstioOperator
$ istioctl install -f ./k8s/istio-operator/overlay.yaml

# Deploy
$ kubectl apply -f ./k8s/sample-app
```