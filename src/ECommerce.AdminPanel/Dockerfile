# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/ECommerce.AdminPanel/ECommerce.AdminPanel.csproj", "src/ECommerce.AdminPanel/"]
COPY ["src/ECommerce.Domain/ECommerce.Domain.csproj", "src/ECommerce.Domain/"]
COPY ["src/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "src/ECommerce.Infrastructure/"]
COPY ["src/ECommerce.Application/ECommerce.Application.csproj", "src/ECommerce.Application/"]
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore "src/ECommerce.AdminPanel/ECommerce.AdminPanel.csproj"
COPY . .
WORKDIR "/src/src/ECommerce.AdminPanel"
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet build "ECommerce.AdminPanel.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish "ECommerce.AdminPanel.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.AdminPanel.dll"]
