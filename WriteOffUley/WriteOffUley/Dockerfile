﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WriteOffUley/WriteOffUley/WriteOffUley.csproj", "WriteOffUley/"]
RUN dotnet restore "WriteOffUley/WriteOffUley.csproj"
COPY . .
WORKDIR "/src/WriteOffUley"
RUN dotnet build "WriteOffUley/WriteOffUley.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WriteOffUley/WriteOffUley.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y postgresql-client
ENV ConnectionStrings__DefaultConnection "Host=dpg-cj18d3qip7vkfo64srjg-a;Port=5432;Database=dbuley;Username=dbuley_user;Password=NDjjsM1f3lf5fYD63cmblPl0MDGBxXlE"

ENTRYPOINT ["dotnet", "WriteOffUley.dll"]
