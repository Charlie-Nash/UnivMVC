# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos del proyecto y restaurar dependencias
COPY ["UnivMVC.Web/*.csproj", "UnivMVC.Web/"]
COPY ["UnivMVC.Application/*.csproj", "UnivMVC.Application/"]
COPY ["UnivMVC.Domain/*.csproj", "UnivMVC.Domain/"]
COPY ["UnivMVC.Infrastructure/*.csproj", "UnivMVC.Infrastructure/"]

RUN dotnet restore "UnivMVC.Web/UnivMVC.Web.csproj"

# Copiar todo y compilar
COPY . .
RUN dotnet publish "UnivMVC.Web/UnivMVC.Web.csproj" -c Release -o /publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "UnivMVC.Web.dll"]