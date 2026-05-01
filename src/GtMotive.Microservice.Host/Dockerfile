# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GtMotive.Microservice.Host/GtMotive.Microservice.Host.csproj", "src/GtMotive.Microservice.Host/"]
COPY ["src/GtMotive.Microservice.Api/GtMotive.Microservice.Api.csproj", "src/GtMotive.Microservice.Api/"]
COPY ["src/GtMotive.Microservice.Application/GtMotive.Microservice.Application.csproj", "src/GtMotive.Microservice.Application/"]
COPY ["src/GtMotive.Microservice.Domain/GtMotive.Microservice.Domain.csproj", "src/GtMotive.Microservice.Domain/"]
COPY ["src/GtMotive.Microservice.Infrastructure/GtMotive.Microservice.Infrastructure.csproj", "src/GtMotive.Microservice.Infrastructure/"]
RUN dotnet restore "./src/GtMotive.Microservice.Host/GtMotive.Microservice.Host.csproj"
COPY . .
WORKDIR "/src/src/GtMotive.Microservice.Host"
RUN dotnet build "./GtMotive.Microservice.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GtMotive.Microservice.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GtMotive.Microservice.Host.dll"]