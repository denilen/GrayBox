FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Facility/Facility.csproj", "Facility/"]
RUN dotnet restore "Facility/Facility.csproj"
COPY . .
WORKDIR "/src/Facility"
RUN dotnet build "Facility.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Facility.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Facility.dll"]
