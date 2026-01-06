# Setup — SchoolManagement.Api ✅

This file contains only the essential setup steps to get the project running locally: required .NET version, packages/tools, DB options, migrations, and commands.

---

## Requirements
- **.NET SDK:** 8.0 or later (verify with `dotnet --info`) 💡
- **Database:** SQL Server (Developer/Express) or LocalDB
- **Tooling:** `dotnet-ef` (global tool) for applying EF Core migrations 🔧

---

## Packages (recommended)
Install these NuGet packages in the `SchoolManagement.Api` project:
- `Microsoft.EntityFrameworkCore.SqlServer` (EF Core provider)
- `Microsoft.EntityFrameworkCore.Design` (design-time tooling)
- `Swashbuckle.AspNetCore` (optional, for Swagger)

Install via CLI (run in project folder):
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Swashbuckle.AspNetCore --version 6.*
```

---

## Connection strings (examples)
- Local SQL Server:
```
Server=.;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True
```
- LocalDB (Visual Studio):
```
Server=(localdb)\MSSQLLocalDB;Database=SchoolDb;Trusted_Connection=True;
```

---

## Common commands
Restore & build:
```bash
dotnet restore
dotnet build
```
Install EF Core tools (only once):
```bash
dotnet tool install --global dotnet-ef
```
Add a migration:
```bash
dotnet ef migrations add <Name> --project SchoolManagement.Api --startup-project SchoolManagement.Api
```
Apply migrations (update database):
```bash
dotnet ef database update --project SchoolManagement.Api --startup-project SchoolManagement.Api
```
Generate an idempotent SQL script:
```bash
dotnet ef migrations script --idempotent -o changes.sql --project SchoolManagement.Api --startup-project SchoolManagement.Api
```
Run the API:
```bash
dotnet run --project SchoolManagement.Api
```
Open Swagger:
```text
https://localhost:<port>/swagger
```

---

## Notes / Checklist ✅
- Ensure there are no existing duplicates if you add unique constraints (run duplicate-check queries first).
- Review generated migrations (`Migrations/`) and the SQL script before applying to shared/dev DBs.
- Seed data added via `OnModelCreating` will be applied when you run `dotnet ef database update`.

---

That's it ✨

