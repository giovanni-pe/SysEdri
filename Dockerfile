FROM  --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src
COPY ["Edri.Api/Edri.Api.csproj", "Edri.Api/"]
COPY ["Edri.Application/Edri.Application.csproj", "Edri.Application/"]
COPY ["Edri.Domain/Edri.Domain.csproj", "Edri.Domain/"]
COPY ["Edri.Shared/Edri.Shared.csproj", "Edri.Shared/"]
COPY ["Edri.Proto/Edri.Proto.csproj", "Edri.Proto/"]
COPY ["Edri.gRPC/Edri.gRPC.csproj", "Edri.gRPC/"]
COPY ["Edri.Infrastructure/Edri.Infrastructure.csproj", "Edri.Infrastructure/"]
RUN dotnet restore "Edri.Api/Edri.Api.csproj" -a $TARGETARCH
COPY . .
WORKDIR "/src/Edri.Api"
RUN dotnet build "Edri.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build -a $TARGETARCH

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
RUN dotnet publish "Edri.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false -a $TARGETARCH

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=5s --start-period=5s --retries=3 \
  CMD curl --fail http://localhost/healthz || exit 1
EXPOSE 80
ENTRYPOINT ["dotnet", "Edri.Api.dll"]
