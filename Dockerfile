# Author: Michael
FROM mcr.microsoft.com/dotnet/sdk@sha256:c0790639332692a0d56cdd81ed581cfd24d040d9839764c138994866df89a3b6 AS build
WORKDIR /src

# Kopier alle .csproj-filer mens mappestrukturen bevares
COPY Semesterprojekt1PBA.Api/Semesterprojekt1PBA.Api.csproj                         Semesterprojekt1PBA.Api/
COPY Semesterprojekt1PBA.Application/Semesterprojekt1PBA.Application.csproj         Semesterprojekt1PBA.Application/
COPY Semesterprojekt1PBA.Domain/Semesterprojekt1PBA.Domain.csproj                   Semesterprojekt1PBA.Domain/
COPY Semesterprojekt1PBA.Infrastructure/Semesterprojekt1PBA.Infrastructure.csproj   Semesterprojekt1PBA.Infrastructure/
COPY Semesterprojekt1PBA.DatabaseMigration/Semesterprojekt1PBA.DatabaseMigration.csproj Semesterprojekt1PBA.DatabaseMigration/
COPY Semesterprojekt1PBA.Presentation/Semesterprojekt1PBA.Presentation.csproj       Semesterprojekt1PBA.Presentation/

# Restore pakker — dette lag caches og køres kun om igen når en .csproj ændres
RUN dotnet restore Semesterprojekt1PBA.Api/Semesterprojekt1PBA.Api.csproj

# Kopier resten af kildekoden
COPY . .

# Byg og publicer i Release-mode
RUN dotnet publish Semesterprojekt1PBA.Api/Semesterprojekt1PBA.Api.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore

# =============================================================
# Stage 2: Runtime
# Kun ASP.NET Core runtime — ingen SDK, intet kildekode.
# Kører som non-root bruger for sikkerhed.
# =============================================================
FROM mcr.microsoft.com/dotnet/aspnet@sha256:8c0b6857eab7b2aa57884c839bf4678414606bd7d17370f18a842ac5cf414711 AS runtime
WORKDIR /app

# Opret en system-bruger og -gruppe der ikke er root
RUN groupadd --system appgroup && \
    useradd --system --gid appgroup --no-create-home appuser

# Kopier den publicerede output fra build-stadiet
COPY --from=build /app/publish .

# Skift til non-root bruger
USER appuser

# Port 8080 er default for ASP.NET Core i .NET 8+
EXPOSE 8080

ENTRYPOINT ["dotnet", "Semesterprojekt1PBA.Api.dll"]