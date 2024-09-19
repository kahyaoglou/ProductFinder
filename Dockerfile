# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Tüm proje dosyalarını kopyala
COPY ProductFinder/ProductFinder.API/ProductFinder.API.csproj ProductFinder/ProductFinder.API/
COPY ProductFinder/ProductFinder.Business/ProductFinder.Business.csproj ProductFinder/ProductFinder.Business/
COPY ProductFinder/ProductFinder.DataAccess/ProductFinder.DataAccess.csproj ProductFinder/ProductFinder.DataAccess/
COPY ProductFinder/ProductFinder.Entities/ProductFinder.Entities.csproj ProductFinder/ProductFinder.Entities/

# Diğer dosyaları ve dizinleri kopyala
COPY ProductFinder/ProductFinder.API/ ProductFinder/ProductFinder.API/
COPY ProductFinder/ProductFinder.Business/ ProductFinder/ProductFinder.Business/
COPY ProductFinder/ProductFinder.DataAccess/ ProductFinder/ProductFinder.DataAccess/
COPY ProductFinder/ProductFinder.Entities/ ProductFinder/ProductFinder.Entities/

# Restore ve build işlemleri
RUN dotnet restore ProductFinder/ProductFinder.API/ProductFinder.API.csproj
WORKDIR /src/ProductFinder/ProductFinder.API
RUN dotnet build ProductFinder.API.csproj -c Release -o /app/build

# Publish aşaması
FROM build AS publish
RUN dotnet publish ProductFinder.API.csproj -c Release -o /app/publish

# Runtime aşaması
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductFinder.API.dll"]
