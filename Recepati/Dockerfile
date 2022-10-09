#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Recepati/Recepati.csproj", "Recepati/"]
RUN dotnet restore "Recepati/Recepati.csproj"
COPY . .
WORKDIR "/src/Recepati"
RUN dotnet build "Recepati.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recepati.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DB_LOCATION=localhost
ENTRYPOINT ["dotnet", "Recepati.dll"]