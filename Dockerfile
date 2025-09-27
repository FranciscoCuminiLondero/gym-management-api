# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GymManagement.Api/GymManagement.Api.csproj", "GymManagement.Api/"]
RUN dotnet restore "GymManagement.Api/GymManagement.Api.csproj"
COPY . .
WORKDIR "/src/GymManagement.Api"
RUN dotnet publish "GymManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GymManagement.Api.dll"]