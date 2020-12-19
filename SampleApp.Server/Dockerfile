FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
EXPOSE 12345
EXPOSE 5432
WORKDIR /opt/app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["SampleApp.Server/SampleApp.Server.csproj", "SampleApp.Server/"]
COPY ["SampleApp.Shared/SampleApp.Shared.csproj", "SampleApp.Shared/"]
RUN dotnet restore "SampleApp.Server/SampleApp.Server.csproj"
COPY . .
WORKDIR "/src/SampleApp.Server"
RUN dotnet build "SampleApp.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SampleApp.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /opt/app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SampleApp.Server.dll"]