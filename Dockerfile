FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_ENVIROMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["prueba.Api/prueba.Api.csproj", "prueba.Api/"]
COPY ["prueba.Application/prueba.Application.csproj", "prueba.Application/"]
COPY ["prueba.Domain/prueba.Domain.csproj", "prueba.Domain/"]
COPY ["prueba.Infrastructure/prueba.Infrastructure.csproj", "prueba.Infrastructure/"]
RUN dotnet restore "prueba.Api/prueba.Api.csproj"
COPY . .
WORKDIR "/src/prueba.Api"
RUN dotnet build "./prueba.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./prueba.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "prueba.Api.dll"]
