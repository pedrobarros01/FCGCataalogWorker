FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /build
COPY ["src/FCG.Catalog.Worker/FCG.Catalog.Worker.csproj", "build/FCG.Catalog.Worker"]
COPY ["src/FCG.Catalog.Application/FCG.Catalog.Application.csproj", "build/FCG.Catalog.Application"]
COPY ["src/FCG.Catalog.Domain/FCG.Catalog.Domain.csproj", "build/FCG.Catalog.Domain"]
COPY ["src/FCG.Catalog.Infrastructure/FCG.Catalog.Infrastructure.csproj", "build/FCG.Catalog.Infrastructure"]
COPY ["src/FCG.Catalog.Shared/FCG.Catalog.Shared.csproj", "build/FCG.Catalog.Shared"]
COPY . .
RUN dotnet restore "build/FCG.Catalog.Worker/FCG.Catalog.Worker.csproj"
WORKDIR "/build/src/FCG.Catalog.Worker"
RUN dotnet build "FCG.Catalog.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCG.Catalog.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet",  "FCG.Catalog.Worker.dll"]