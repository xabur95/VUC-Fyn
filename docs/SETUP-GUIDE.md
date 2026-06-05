# VUC-Fyn — Opsætningsguide

Denne guide får dig fra et tomt repo-clone til en fuldt kørende lokal udviklingsstack med API, database, monitoring og logging.

---

## Forudsætninger

følgende skal køre på din pc inden du går i gang:

- Docker Desktop

---


## Trin 1 — Opret din lokale `.env` fil

`.env` filen indeholder dine lokale adgangskoder til databasen og Grafana. Den committes **aldrig** til Git — det er din personlige fil.

```powershell
cp .env.example .env
```

Åbn `.env` i VS Code:

```powershell
code .env
```

Filen ser sådan ud:

```
DB_PASSWORD=ChangeMe123!
GRAFANA_PASSWORD=ChangeMe123!
```

Erstat begge værdier med aftalte passwords fra gruppen. **SQL Server kræver minimum 8 tegn med store og små bogstaver, tal og specialtegn.**

Eksempel på gyldige passwords:

```
DB_PASSWORD=VucFyn2026!
GRAFANA_PASSWORD=VucFyn2026!
```

> **Vigtigt:** `ChangeMe123!` virker ikke — SQL Server afviser passwords der indeholder ordet "password" eller er for simple. Brug passwords som eksemplet ovenfor.

---

## Trin 2 — Start stacken

```powershell
docker compose up --build
```

Første gang tager det 3-5 minutter da Docker skal downloade alle images. Efterfølgende starter det på under 30 sekunder.

Når du ser denne linje er API'en klar:

```
api-1  | Now listening on: http://[::]:8080
```

Vil du køre i baggrunden (anbefales til daglig brug):

```powershell
docker compose up --build -d
```

---

## Trin 3 — Verificer at alt kører

Åbn disse URLer i din browser:

| Service | URL | Beskrivelse |
|---|---|---|
| **API** | http://localhost:5000 | VUC-Fyn REST API |
| **Metrics** | http://localhost:5000/metrics | Prometheus metrics fra API'en |
| **Prometheus** | http://localhost:9090 | Prometheus query UI |
| **Grafana** | http://localhost:3000 | Dashboards og logs |
| **cAdvisor** | http://localhost:8081 | Container resource metrics |
| **Node Exporter** | http://localhost:9100/metrics | Host metrics |
| **Loki** | http://localhost:3100 | Log aggregering (ingen browser UI) |

---

## Trin 4 — Opsæt Grafana datasources (kun første gang)

Log ind på Grafana: http://localhost:3000

- Brugernavn: `admin`
- Password: dit `GRAFANA_PASSWORD` fra `.env`

### Tilføj Prometheus

1. Gå til **Connections → Data sources → Add data source**
2. Vælg **Prometheus**
3. URL: `http://prometheus:9090`
4. Klik **Save & test** — skal vise grøn checkmark

### Tilføj Loki

1. Gå til **Connections → Data sources → Add data source**
2. Vælg **Loki**
3. URL: `http://loki:3100`
4. Klik **Save & test** — skal vise grøn checkmark

> Bemærk: URL'erne bruger container-navne (`prometheus`, `loki`) ikke `localhost`, fordi Grafana kommunikerer internt i Docker-netværket.

---

## Daglige kommandoer

### Start stacken

```powershell
docker compose up -d
```

### Stop stacken

```powershell
docker compose down
```

### Se logs fra en specifik container

```powershell
docker logs vuc-fyn-api-1
docker logs vuc-fyn-db-1
docker logs vuc-fyn-loki-1
```

### Se alle kørende containers

```powershell
docker ps
```

### Genbyg API'en efter kodeændringer

```powershell
docker compose up --build -d
```

### Stop og slet alt data (clean slate)

```powershell
docker compose down -v
```

> **Advarsel:** `-v` sletter også SQL Server data. Brug kun dette hvis du vil starte helt forfra.

---

## Containers i stacken

| Container | Formål | Port |
|---|---|---|
| `vuc-fyn-api-1` | ASP.NET Core API | 5000 |
| `vuc-fyn-db-1` | SQL Server 2022 | 1433 |
| `vuc-fyn-prometheus-1` | Metrics scraper og storage | 9090 |
| `vuc-fyn-grafana-1` | Visualisering og dashboards | 3000 |
| `vuc-fyn-loki-1` | Log aggregering | 3100 |
| `vuc-fyn-promtail-1` | Log indsamler (ingen ekstern port) | — |
| `vuc-fyn-node-exporter-1` | Host metrics | 9100 |
| `vuc-fyn-cadvisor-1` | Container metrics | 8081 |

---

## Fejlfinding

### API starter ikke

API'en venter på at databasen er klar. SQL Server bruger 20-30 sekunder på at initialisere første gang. Vent lidt og tjek:

```powershell
docker logs vuc-fyn-db-1
```

Når du ser `SQL Server is now ready for client connections` er databasen klar.

### Port allerede i brug

Hvis du får fejlen `port already allocated` kører en anden container sandsynligvis på samme port. Tjek hvad der kører:

```powershell
docker ps
```

Stop den conflicting container eller skift port i `docker-compose.yml`.

### Grafana viser ingen data

Tjek at Prometheus og Loki datasources er korrekt konfigureret (Trin 5). Klik **Save & test** på begge og verificer at de viser grøn checkmark.

### Glemt Grafana password

Stop stacken og slet Grafana's data volume:

```powershell
docker compose down
docker volume rm vuc-fyn_grafana_data
docker compose up -d
```

Log derefter ind med dit `GRAFANA_PASSWORD` fra `.env` og opsæt datasources igen (Trin 5).

### `.env` password virker ikke

SQL Server kræver et stærkt password. Krav:
- Minimum 8 tegn
- Mindst ét stort bogstav
- Mindst ét lille bogstav
- Mindst ét tal eller specialtegn
- Må **ikke** indeholde ordet "password"

---

## Filer du skal kende

```
VUC-Fyn/
├── Dockerfile              # Multi-stage build til API'en
├── .dockerignore           # Filer der ekskluderes fra Docker build
├── docker-compose.yml      # Definerer alle 8 services
├── .env.example            # Skabelon — kopiér til .env og udfyld
├── .env                    # Dine lokale passwords (committes ikke!)
└── monitoring/
    ├── prometheus.yml       # Prometheus scrape konfiguration
    ├── loki-config.yml      # Loki log storage konfiguration
    └── promtail-config.yml  # Promtail log indsamlings konfiguration
```

---

## Vigtige regler

- `.env` committes **aldrig** til Git — den er i `.gitignore`
- Del passwords med teamet via Teams/Discord, ikke via Git
- Brug altid feature branches — push **aldrig** direkte til `main`
- En PR mod `main` trigger automatisk CI pipeline med tests
