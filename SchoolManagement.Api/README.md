# SchoolManagement.Api

A small, production-minded ASP.NET Core Web API for managing Students, Courses, Departments and Enrollments.

This repository contains:
- Controllers that expose REST endpoints under `/api/*` (Courses, Students, Departments, Enrollments, Reports)
- Services with business rules and validations
- Repositories that access the database using EF Core and implement paging/filtering/sorting
- Mappings (Mapster) for DTO ↔ Entity conversions
- Middleware for consistent error handling, correlation ids, and request logging
- EF Core migrations to manage schema changes

---

## Table of contents
- [Quick start](#-quick-start)
- [Project structure](#-project-structure)
- [Database & schema](#-database--schema)
- [API & error format](#-api--error-format)
- [Testing](#-testing)
- [Local development notes](#-local-development-notes)
- [Roadmap](#-roadmap)

---

## 🔧 Quick start

Prerequisites
- .NET 8 SDK
- SQL Server (local instance, Docker container, or LocalDB)

Clone and run the app locally:
```bash
git clone <repo-url>
cd "daywise C#\SchoolManagement.Api"
# restore + build
dotnet restore && dotnet build
# create/update database
dotnet ef database update
# run
dotnet run
```
The API is available at `https://localhost:<port>` and Swagger UI at `/swagger`.

> Note: the app runs EF migrations automatically at startup if it detects pending migrations.

---

## 📁 Project structure (short)
- `Controllers/` - API endpoints
- `Services/` - business logic & validations
- `Repositories/` - EF Core data access, paging & sorting
- `Data/` - `AppDbContext` and migrations
- `DTOs/` - request/response contracts and `ApiResponse` wrapper
- `Middleware/` - `CorrelationId`, `ExceptionHandling`, `RequestLogging`
- `Mappings/` - Mapster configuration
- `Swagger/` - schema examples for the UI

---

## 🗄 Database & schema (summary)
See `docs/DATABASE_SCHEMA.md` for a complete, human-friendly schema overview, relationships, indexes and sample SQL checks to verify integrity.

Key highlights:
- **Unique constraints**: `IX_Courses_Title` (Course.Title), `IX_Departments_Name` (Department.Name)
- **Enrollment uniqueness**: composite unique index on `(StudentId, CourseId)` to prevent duplicate enrollments
- **Relationships and cascade rules**:
  - `Enrollment` → `Student` and `Course`: `OnDelete(DeleteBehavior.Cascade)` (deleting Student or Course removes related Enrollments)
  - `Student.Course -> Department` and `Course -> Department`: `OnDelete(DeleteBehavior.SetNull)` (service prevents deletion if children exist)

---

## 📡 API & error format
All responses use a standard wrapper `ApiResponse<T>` or non-generic `ApiResponse` for errors:
- `success` boolean
- `data` (optional) for successful responses
- `message` for errors
- `status` HTTP status code
- `timestamp` UTC
- `meta` (optional) for additional metadata (e.g., `CorrelationId`, field-level errors)

Example error on duplicate resource:
```json
{
  "success": false,
  "message": "Department with the same name already exists.",
  "status": 409,
  "meta": { "CorrelationId": "00-...", "Errors": { "Name": "'Cyber Security' is already in use" }}
}
```
Middleware ensures:
- Consistent mapping of exceptions to HTTP status codes (400/404/409/500)
- Correlation id header (`X-Correlation-ID`) on responses for tracing
- Sanitized messages (no raw SQL leaks) while preserving friendly messages for clients

---

## ✅ Testing & Postman
- Use your Postman collection organized by controller. Important test groups:
  - Happy-path CRUD for all controllers
  - Validation tests (missing/invalid fields → 400)
  - Not-found tests (GET/PUT/DELETE unknown id → 404)
  - Conflict tests (duplicate name/title → 409) and check `meta.errors`
  - Pagination & sorting edge cases
- Consider adding Newman to CI to run the collection in automated pipelines.

---

## 🧰 Local development notes
- Migrations are in `Migrations/` — add new migrations using `dotnet ef migrations add <Name>` and apply with `dotnet ef database update`.
- Avoid using EF InMemory as the single verification for DB constraints (it does not enforce relational constraints). Use LocalDB or Docker SQL Server for integration tests.
- Add secrets to the environment or use `dotnet user-secrets` in development to keep sensitive data out of source control.

---

## 📌 Roadmap (recommended)
- Add `ValidationFilter` to standardize model validation errors (400) into `ApiResponse` body
- Add integration tests for concurrency / unique constraints
- Add Swagger examples for standard error responses
- Add optional authentication (JWT) if endpoints should be protected

---

If you want, I can add the Postman collection and environment to `tests/postman/` and add a sample Newman command for CI.

---

*If you'd like wording tweaks or extra examples (curl snippets, more questions), tell me which sections to expand.*
