FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
EXPOSE 12345
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["VeldtAR.Shared/VeldtAR.Shared.csproj", "VeldtAR.Shared/"]
COPY ["VeldtAR.Server/VeldtAR.Server.csproj", "VeldtAR.Server/"]
RUN dotnet restore "VeldtAR.Server/VeldtAR.Server.csproj"
COPY . .
WORKDIR "/src/VeldtAR.Server"
RUN dotnet build "VeldtAR.Server.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "VeldtAR.Server.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VeldtAR.Server.dll"]
