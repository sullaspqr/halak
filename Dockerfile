# .NET SDK a buildhez
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Az összes fájl másolása a konténerbe
COPY . .

# Restore és build
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# .NET runtime a futtatáshoz
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Alkalmazás indítása
ENTRYPOINT ["dotnet", "halak.dll"]
