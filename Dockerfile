FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /build
COPY ["src/FCG.Catalog.Worker/FCG.Catalog.Worker.csproj", "build/FCG.Catalog.Worker"]
COPY ["src/FCG.Catalog.Worker.Application/FCG.Catalog.Worker.Application.csproj", "build/FCG.Catalog.Worker.Application"]
COPY ["src/FCG.Catalog.Worker.Domain/FCG.Catalog.Worker.Domain.csproj", "build/FCG.Catalog.Worker.Domain"]
COPY ["src/FCG.Catalog.Worker.Infrastructure/FCG.Catalog.Worker.Infrastructure.csproj", "build/FCG.Catalog.Worker.Infrastructure"]
COPY ["src/FCG.Shared/FCG.Shared.csproj", "build/FCG.Shared"]
COPY . .
RUN dotnet restore "/build/src/FCG.Catalog.Worker/FCG.Catalog.Worker.csproj"
WORKDIR "/build/src/FCG.Catalog.Worker"
RUN dotnet build "FCG.Catalog.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCG.Catalog.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet",  "FCG.Catalog.Worker.dll"]