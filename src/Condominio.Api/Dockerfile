#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Condominio.Api/Condominio.Api.csproj", "src/Condominio.Api/"]
COPY ["src/Condominio.Data/Condominio.Data.csproj", "src/Condominio.Data/"]
COPY ["src/Condominio.Domain/Condominio.Domain.csproj", "src/Condominio.Domain/"]
COPY ["src/Condominio.Util/Condominio.Util.csproj", "src/Condominio.Util/"]
COPY ["src/Condominio.Service/Condominio.Service.csproj", "src/Condominio.Service/"]
RUN dotnet restore "src/Condominio.Api/Condominio.Api.csproj"
COPY . .
WORKDIR "/src/src/Condominio.Api"
RUN dotnet build "Condominio.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Condominio.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Condominio.Api.dll"]