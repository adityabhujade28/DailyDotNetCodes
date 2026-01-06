# Setup Guide — SchoolManagement.Api

This setup guide walks through local development, database setup, and common troubleshooting tips.

---

## Prerequisites
- .NET 8 SDK (check with `dotnet --info`)
- SQL Server (Developer/Express), LocalDB or Docker-based SQL Server
- Optional: Postman for manual API tests

---

## Local DB options
1. **Local SQL Server**
   - Use `Server=.;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True`
2. **LocalDB (Visual Studio)**
   - `Server=(localdb)\MSSQLLocalDB;Database=SchoolDb;Trusted_Connection=True;`
3. **Docker SQL Server** (recommended for isolated dev):
   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=YourStrong!Passw0rd' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
   # connection string example
   # Server=localhost,1433;Database=SchoolDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;
   ```

---

## Recommended local workflow
1. Create `.env` or use user-secrets to store `ConnectionStrings:DefaultConnection` (avoid committing credentials)
2. Restore & build
   ```bash
dotnet restore
dotnet build
```
3. Apply migrations
   ```bash
dotnet tool install --global dotnet-ef # if first time
dotnet ef database update
```
4. Run the API
   ```bash
dotnet run
```
5. Visit Swagger at `https://localhost:<port>/swagger` to explore endpoints

---

## Running the Postman collection (recommended)
- Import your exported collection and environment into Postman.
- Run the collection via Runner for smoke tests or via Newman in CI:
  ```bash
  newman run tests/postman/school-api.postman_collection.json -e tests/postman/school-api.env.json
  ```

---

## Troubleshooting
- **Cannot connect to DB**: verify SQL Server is running, firewall allows connections, connection string is correct and account has permissions.
- **EF migrations fail**: check for existing duplicate data that would violate new unique constraints (run duplicate detection queries first).
- **404 on known endpoint**: ensure app is running on the expected port and `UseSwagger`/`MapControllers` are active in `Program.cs`.
- **Error shows CorrelationId**: use the correlation id in server logs (RequestLogging) to find the correlation in logs for deeper debugging.

---

## Checklist before committing DB-related changes
- Run duplicate checks for columns with new unique constraints
- Create migration, review SQL script (`dotnet ef migrations script --idempotent -o changes.sql`)
- Apply migration to a dev DB and run integration tests

---

If you want, I can add a `docker-compose.yml` to the repo to make spinning up SQL Server for dev and CI trivial. Would you like that?