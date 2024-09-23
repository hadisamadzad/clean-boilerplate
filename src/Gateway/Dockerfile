FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . .
RUN rm -rf ./src/Gateway
COPY src/Gateway src/Gateway
RUN dotnet publish src/Gateway -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT dotnet Gateway.dll