FROM mcr.microsoft.com/dotnet/aspnet:8.0.2-jammy-chiseled-amd64 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore -r linux-x64
RUN dotnet build -r linux-x64 -c Release --no-restore --no-self-contained
RUN dotnet publish -r linux-x64 -c Release -o publish --no-restore --no-build

FROM base AS final
WORKDIR /app
COPY --from=build /src/publish .
ENTRYPOINT ["dotnet", "RinhaBackend202401.WebApi.dll"]