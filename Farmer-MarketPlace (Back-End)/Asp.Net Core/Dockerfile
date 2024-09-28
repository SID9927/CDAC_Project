FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DotnetBackend.csproj", "./"]
RUN dotnet restore "DotnetBackend.csproj"
COPY . .
RUN dotnet build "DotnetBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetBackend.dll"]
