FROM mcr.microsoft.com/dotnet/aspnet:6.0.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["api/BlueHarvest.Api/BlueHarvest.Api.csproj", "api/BlueHarvest.Api/"]

COPY ["modules/transactions/BlueHarvest.Modules.Transactions.Api/BlueHarvest.Modules.Transactions.Api.csproj", "modules/transactions/BlueHarvest.Modules.Transactions.Api/"]
COPY ["modules/transactions/BlueHarvest.Modules.Transactions.Core/BlueHarvest.Modules.Transactions.Core.csproj", "modules/transactions/BlueHarvest.Modules.Transactions.Core/"]

COPY ["modules/users/BlueHarvest.Modules.Users.Api/BlueHarvest.Modules.Users.Api.csproj", "modules/users/BlueHarvest.Modules.Users.Api/"]
COPY ["modules/users/BlueHarvest.Modules.Users.Core/BlueHarvest.Modules.Users.Core.csproj", "modules/users/BlueHarvest.Modules.Users.Core/"]


COPY ["shared/BlueHarvest.Shared.Application/BlueHarvest.Shared.Application.csproj", "shared/BlueHarvest.Shared.Application/"]
COPY ["shared/BlueHarvest.Shared.Infrastructure/BlueHarvest.Shared.Infrastructure.csproj", "shared/BlueHarvest.Shared.Infrastructure/"]

RUN dotnet restore "api/BlueHarvest.Api/BlueHarvest.Api.csproj"

COPY . .
WORKDIR "/src/api/BlueHarvest.Api"
RUN dotnet build "BlueHarvest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlueHarvest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlueHarvest.Api.dll"]
