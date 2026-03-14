# Projektkontext — Semesterprojekt1PBA

> Denne fil er lokal og ikke committed til Git (.gitignore).

## Projekt

**Repo:** `xabur95/VUC-Fyn`
**Løsning:** `Semesterprojekt1PBA.slnx`
**Eksamen:** om ca. 3 måneder — fokus på at demonstrere SOLID og DDD

---

## Arkitektur — Onion Architecture

| Projekt | Rolle |
|---|---|
| `Semesterprojekt1PBA.Domain` | Domænemodeller, Value Objects, interfaces |
| `Semesterprojekt1PBA.Application` | Use cases, CQS (Commands/Queries) |
| `Semesterprojekt1PBA.DatabaseMigrations` | EF Core migrations, TPH-konfiguration |
| `Semesterprojekt1PBA.Infrastructure` | Repositories, DbContext |
| `Semesterprojekt1PBA.Api` | Controllers, DI-opsætning |
| `Semesterprojekt1PBA.Domain.Tests` | Unit tests af domænelag |

---

## Designprincipper

- **DDD**: Entities, Value Objects, Aggregates
- **SOLID**: Eksamen fokuserer på at demonstrere disse i praksis
- **TPH** (Table Per Hierarchy): EF Core for User-hierarkiet
- **CQS** (Command Query Separation): i Application-laget

---

## Domænemodel

### User-hierarkiet
`User` er en **abstrakt base class** i `Domain/Entities/`.

Konkrete subklasser (arver fra User):
- `Student`
- `Teacher`
- `Admin`

`DomainEntity` er abstract base class der giver `Id` (Guid) til alle entities.

### Value Objects (`Domain/ValueObjects/`)
- `UserName` — indeholder `FirstName` og `LastName` med fælles valideringslogik, implementeret som `record`
- `Email` — validerer e-mailadresse

### Testnavn-konvention
`Method_WhenCondition_ExpectedResult`

---

## Status (opdatér løbende)

- [x] `DomainEntity` — abstract base class med Id
- [x] `User` — abstract entity med Name og Email
- [x] `UserName` — Value Object under udarbejdelse
- [ ] `Email` — Value Object
- [ ] `Student`, `Teacher`, `Admin` — konkrete subklasser
- [ ] `IUserRepository` — interface i Domain
- [ ] Infrastructure: DbContext, TPH-konfiguration
- [ ] Application: Commands og Queries (CQS)
- [ ] API: Controllers og DI
