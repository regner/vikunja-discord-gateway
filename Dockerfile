FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["VikunjaDiscordGateway.csproj", "./"]
RUN dotnet restore "VikunjaDiscordGateway.csproj"

COPY . .
RUN dotnet build "./VikunjaDiscordGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "VikunjaDiscordGateway.dll"]