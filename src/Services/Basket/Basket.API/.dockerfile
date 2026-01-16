FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5001
EXPOSE 5051

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["src/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "BuildingBlocks.Messaging/"]
COPY ["src/Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]

RUN dotnet restore "./Services/Basket/Basket.API/Basket.API.csproj"

COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
COPY ["src/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "BuildingBlocks.Messaging/"]
COPY ["src/Services/Basket", "Services/Basket/"]

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Services/Basket/Basket.API
RUN dotnet publish "./Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
# USER root
# COPY src/Services/Basket/Basket.API/client.crt /usr/local/share/ca-certificates/client.crt
# RUN chmod 644 /usr/local/share/ca-certificates/client.crt && update-ca-certificates
# USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]