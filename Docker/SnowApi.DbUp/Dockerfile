#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM docker-images.artifactory.danskenet.net/db/dotnet/core/aspnet:8.0 AS base

WORKDIR /app

FROM remote-docker-mcr.artifactory.danskenet.net/dotnet/sdk:8.0 AS build

WORKDIR /

RUN mkdir certs
RUN curl http://artifactory.danskenet.net/artifactory/db-generic-AzureDevops/prerequisites/ConfigFiles/DanskeBankRootCA.cer --output ./certs/root_ca.cer
RUN openssl x509 -inform DER -in ./certs/root_ca.cer -out /usr/local/share/ca-certificates/DanskeBankRootCA.crt
RUN curl http://artifactory.danskenet.net/artifactory/db-generic-AzureDevops/prerequisites/ConfigFiles/DanskeBankIssuingCA1.cer --output ./certs/ca1.cer
RUN openssl x509 -inform DER -in ./certs/ca1.cer -out /usr/local/share/ca-certificates/DanskeBankIssuingCA1.crt
RUN update-ca-certificates

COPY ["./SnowApi.DbUp/*.csproj", "SnowApi.DbUp/"]
COPY ["./Docker/SnowApi.DbUp/nuget.config","SnowApi.DbUp/"]

WORKDIR /SnowApi.DbUp

RUN dotnet nuget remove source nuget.org
RUN dotnet restore "SnowApi.DbUp.csproj" --configfile "nuget.config"

WORKDIR /

# Copy source files
COPY ["./SnowApi.DbUp/", "SnowApi.DbUp/"]

WORKDIR /SnowApi.DbUp
RUN dotnet build "SnowApi.DbUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SnowApi.DbUp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnowApi.DbUp.dll"]