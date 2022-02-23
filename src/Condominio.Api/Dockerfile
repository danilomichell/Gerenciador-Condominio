#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Imdb.Api/Imdb.Api.csproj", "src/Imdb.Api/"]
COPY ["src/Imdb.Data/Imdb.Data.csproj", "src/Imdb.Data/"]
COPY ["src/Imdb.Domain/Imdb.Domain.csproj", "src/Imdb.Domain/"]
COPY ["src/Imdb.Service/Imdb.Service.csproj", "src/Imdb.Service/"]
COPY ["src/Imdb.Util/Imdb.Util.csproj", "src/Imdb.Util/"]
RUN dotnet restore "src/Imdb.Api/Imdb.Api.csproj"
COPY . .
WORKDIR "/src/src/Imdb.Api"
RUN dotnet build "Imdb.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Imdb.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imdb.Api.dll"]
