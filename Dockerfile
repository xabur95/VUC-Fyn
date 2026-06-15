# Author: Michael
# Stage 1: Build: SDK bruges kun her til at kompilere; det fjernes helt fra det endelige image
# SHA digest i stedet for tag — garanterer nøjagtig samme image ved hvert build
FROM mcr.microsoft.com/dotnet/sdk@sha256:c0790639332692a0d56cdd81ed581cfd24d040d9839764c138994866df89a3b6 AS build
WORKDIR /src

# Cache-optimering: .csproj kopieres FØR kildekode
# → restore-laget genbruges hvis ingen pakker er ændret
COPY Semesterprojekt1PBA.Api/Semesterprojekt1PBA.Api.csproj                         Semesterprojekt1PBA.Api/
COPY Semesterprojekt1PBA.Application/Semesterprojekt1PBA.Application.csproj         Semesterprojekt1PBA.Application/
COPY Semesterprojekt1PBA.Domain/Semesterprojekt1PBA.Domain.csproj                   Semesterprojekt1PBA.Domain/
COPY Semesterprojekt1PBA.Infrastructure/Semesterprojekt1PBA.Infrastructure.csproj   Semesterprojekt1PBA.Infrastructure/
COPY Semesterprojekt1PBA.DatabaseMigration/Semesterprojekt1PBA.DatabaseMigration.csproj Semesterprojekt1PBA.DatabaseMigration/
COPY Semesterprojekt1PBA.Presentation/Semesterprojekt1PBA.Presentation.csproj       Semesterprojekt1PBA.Presentation/

# Nuget pakker hentes(fra .csproj i cache optimering) og caches som eget layer
RUN dotnet restore Semesterprojekt1PBA.Api/Semesterprojekt1PBA.Api.csproj

# Kildekoden kopieres EFTER restore og bygges
COPY . .
# Kompiler og publicer applikationen i Release-mode
RUN dotnet publish Semesterprojekt1PBA.Api/Semesterprojekt1PBA.Api.csproj \
    --configuration Release \  # Byg i Release-mode (optimeret, ingen debug-info)
    --output /app/publish \    # Læg det færdige output i /app/publish
    --no-restore               # Spring restore over — det er allerede gjort ovenfor!

# Stage 2: Runtime — kun ASP.NET Core runtime; ingen SDK og intet kildekode inkluderes i det endelige image
FROM mcr.microsoft.com/dotnet/aspnet@sha256:8c0b6857eab7b2aa57884c839bf4678414606bd7d17370f18a842ac5cf414711 AS runtime
WORKDIR /app

# Non-root user — least privilege i produktion
RUN groupadd --system appgroup && \
    useradd --system --gid appgroup --no-create-home appuser

# Kopier kun det kompilerede output fra Stage 1 — ingen kildekode eller SDK
COPY --from=build /app/publish .

# Kør aldrig som root
USER appuser

EXPOSE 8080

ENTRYPOINT ["dotnet", "Semesterprojekt1PBA.Api.dll"]