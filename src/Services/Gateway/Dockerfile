FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# Run 'docker build' command from the solution directory
WORKDIR /app
COPY . .
RUN rm -rf ./src/Services
COPY src/Services/Gateway src/Services/Gateway
RUN dotnet publish src/Services/Gateway -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT dotnet Gateway.dll