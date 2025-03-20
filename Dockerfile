# Használt .NET verzió
ARG DOTNET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release

# Build környezet létrehozása
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS build

# Munkakönyvtár beállítása és fájlok másolása
WORKDIR /src
COPY ["halak.sln", "."]
COPY ["halak/", "halak/"]

# Restore, build és publish lépések
WORKDIR /src/halak
RUN dotnet restore
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish -r linux-musl-x64

# Futási környezet létrehozása
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS run
WORKDIR /app
COPY --from=build /app/publish .

# Alkalmazás indítása
ENTRYPOINT ["dotnet", "halak.dll"]
