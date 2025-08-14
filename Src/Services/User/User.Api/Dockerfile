# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Services/User/User.Api/User.Api.csproj", "Src/Services/User/User.Api/"]
COPY ["Src/Services/User/User.Application/User.Application.csproj", "Src/Services/User/User.Application/"]
COPY ["Src/Services/User/User.Domain/User.Domain.csproj", "Src/Services/User/User.Domain/"]
COPY ["Src/Services/User/User.Infrastructure/User.Infrastructure.csproj", "Src/Services/User/User.Infrastructure/"]
COPY ["Src/Services/User/User.Persistence/User.Persistence.csproj", "Src/Services/User/User.Persistence/"]
RUN dotnet restore "./Src/Services/User/User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/User/User.Api"
RUN dotnet build "./User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]