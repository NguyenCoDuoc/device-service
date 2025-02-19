# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Portal.API/Device.API.csproj", "src/Device.API/"]
COPY ["Portal.Common/Device.Common.csproj", "Device.Common/"]
COPY ["src/Portal.Application/Device.Application.csproj", "src/Device.Application/"]
COPY ["src/Portal.Domain/Device.Domain.csproj", "src/Device.Domain/"]
RUN dotnet restore "./src/Device.API/Device.API.csproj"
COPY . .
WORKDIR "/src/src/Portal.API"
RUN dotnet build "./Device.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Device.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Device.API.dll"]