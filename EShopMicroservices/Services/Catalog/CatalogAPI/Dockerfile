FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/Catalog/CatalogAPI/CatalogAPI.csproj", "Services/Catalog/CatalogAPI/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "Services/Catalog/CatalogAPI/CatalogAPI.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/CatalogAPI"
RUN dotnet build "CatalogAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CatalogAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]
