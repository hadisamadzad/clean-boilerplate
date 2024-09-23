FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# Run 'docker build' command from the solution directory
WORKDIR /app
COPY . .
RUN rm -rf ./src
COPY src/Identity src/Identity
RUN dotnet publish src/Identity -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT dotnet Identity.dll