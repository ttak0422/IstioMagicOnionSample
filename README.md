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

## Info

- My Environment

  ```
  $ uname
  Darwin

  $ kubectx
  minikube

  $ dotnet --version
  3.1.402

  $ kubectl version
  Client Version: version.Info{Major:"1", Minor:"19", GitVersion:"v1.19.1", GitCommit:"206bcadf021e76c27513500ca24182692aabd17e", GitTreeState:"archive", BuildDate:"1980-01-01T00:00:00Z", GoVersion:"go1.15.2", Compiler:"gc", Platform:"darwin/amd64"}
  Server Version: version.Info{Major:"1", Minor:"19", GitVersion:"v1.19.0", GitCommit:"e19964183377d0ec2052d1f1fa930c4d7575bd50", GitTreeState:"clean", BuildDate:"2020-08-26T14:23:04Z", GoVersion:"go1.15", Compiler:"gc", Platform:"linux/amd64"}

  $ istioctl version
  client version: 1.7.0
  control plane version: 1.7.0
  data plane version: 1.7.0 (3 proxies)
  ```
