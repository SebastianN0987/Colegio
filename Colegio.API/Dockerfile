FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Colegio.API/Colegio.API.csproj", "Colegio.API/"]
COPY ["Colegio/Colegio.Modelos.csproj", "Colegio.Modelos/"]
RUN dotnet restore "./Colegio.API/Colegio.API.csproj"
COPY . .
WORKDIR "/src/Colegio.API"
RUN dotnet build "./Colegio.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Colegio.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Colegio.API.dll"]
