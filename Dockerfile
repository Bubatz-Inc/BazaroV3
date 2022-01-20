#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bazaro.Web/Bazaro.Web.csproj", "Bazaro.Web/"]
RUN dotnet restore "Bazaro.Web/Bazaro.Web.csproj"
COPY . .
WORKDIR "/src/Bazaro.Web"

RUN dotnet build "Bazaro.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bazaro.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bazaro.Web.dll"]
