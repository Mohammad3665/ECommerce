# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5162

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/ECommerce.Api/ECommerce.Api.csproj", "src/ECommerce.Api/"]
COPY ["src/ECommerce.Domain/ECommerce.Domain.csproj", "src/ECommerce.Domain/"]
COPY ["src/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "src/ECommerce.Infrastructure/"]
COPY ["src/ECommerce.Application/ECommerce.Application.csproj", "src/ECommerce.Application/"]
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore "src/ECommerce.Api/ECommerce.Api.csproj"
COPY . .
WORKDIR "/src/src/ECommerce.Api"
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet build "ECommerce.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish "ECommerce.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.dll"]
