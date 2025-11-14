# BUILD
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY WebApp.sln .

# Copiar cada proyecto individualmente
COPY WebApi/WebApi.csproj WebApi/
COPY WebApp/WebApp.csproj WebApp/
COPY Shared/Domain.csproj Shared/
COPY Data/Data.csproj Data/
COPY Business/Business.csproj Business/
COPY WebAppPxy/Service.csproj WebAppPxy/

RUN dotnet restore WebApp.sln

#SE COPIA TODO EL CODIGO
COPY . .

WORKDIR /src/WebApi
RUN dotnet publish -c Release -o /app/publish

# RUNTIME
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
