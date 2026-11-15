# ToDoList Fullstack

Task manager: React client → .NET 8 Minimal API → MySQL 8 (Docker).
EF Core, DB-first (scaffolded from the database).

## Prerequisites

- Docker
- .NET 8 SDK
- Node.js 18+

## Run

From the repository root, in three terminals:

```bash
# 1. Database (first start takes ~30s; db/init.sql creates the Items table)
docker compose up -d

# 2. API
cd TodoApi
dotnet run

# 3. Client
cd client
npm i
npm start
```

| What | URL |
|---|---|
| Client | http://localhost:3000 |
| Swagger | http://localhost:5220/swagger |
| Adminer | http://localhost:8081 — server `mysql`, user `root`, password `1234`, database `ToDoDB` |

## API

| Method | Route | Notes |
|---|---|---|
| GET | `/items` | All tasks (no tracking) |
| POST | `/items` | Body: `{ name, isComplete }` → 201 with the created task |
| PUT | `/items/{id}` | Body: `{ isComplete, name? }`; `name` is kept if omitted → 204 / 404 |
| DELETE | `/items/{id}` | 204 / 404 |

## Structure

```
TodoApi/            Minimal API (Program.cs), EF models in Models/ (scaffolded)
client/src/         React app; API calls in service.js (axios baseURL + error interceptor)
db/init.sql         Schema, runs on the first container start
docker-compose.yml  MySQL 8.0 + Adminer
```

## Troubleshooting

- **Port 3306 in use:** change the left port in `docker-compose.yml` and the port in `TodoApi/appsettings.json`.
- **API fails to connect:** the database is still initializing; wait and retry.
- **Reset the database:** `docker compose down -v`, then `docker compose up -d`.
