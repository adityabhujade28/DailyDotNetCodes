# Database schema (SchoolManagement.Api)

This document summarizes the main tables, columns, constraints, indexes, and relationships used by the API. Use this when reviewing data model decisions or when running schema verification checks.

---

## Tables & important columns

### Departments
- Id (int, PK)
- Name (nvarchar(100), NOT NULL) — **unique** (`IX_Departments_Name`)
- Navigation: Students, Courses

### Students
- Id (int, PK)
- Name (nvarchar(100), NOT NULL)
- Email (nvarchar(150), NOT NULL)
- PhoneNumber (nvarchar(20), NULL)
- DepartmentId (int, NULL) — FK -> Departments(Id) with `OnDelete(SetNull)`
- CreatedAt (datetime) default UTC

> Note: Email is currently not unique; add unique index if business requires it.

### Courses
- Id (int, PK)
- Title (nvarchar(100), NOT NULL) — **unique** (`IX_Courses_Title`)
- Description (nvarchar(500), NULL)
- Credits (int)
- DepartmentId (int, NULL) — FK -> Departments(Id) with `OnDelete(SetNull)`

### Enrollments
- Id (int, PK)
- StudentId (int, NOT NULL) — FK -> Students(Id) with `OnDelete(Cascade)`
- CourseId (int, NOT NULL) — FK -> Courses(Id) with `OnDelete(Cascade)`
- EnrollmentDate (datetime)
- Grade (nvarchar(??), NULL)
- NumericGrade (decimal(5,2), NULL) — precision set in `OnModelCreating`
- **Unique** composite index on `(StudentId, CourseId)` to prevent duplicate enrollments

---

## Indexes & constraints (summary)
- `IX_Departments_Name` (unique) — Department.Name
- `IX_Courses_Title` (unique) — Course.Title
- `IX_Enrollments_StudentId_CourseId` (unique) — Enrollment composite (StudentId, CourseId)
- FK indexes for lookup efficiency: `CourseId`, `DepartmentId`, `StudentId`

---

## Relationships & cascade behaviors
- Enrollment → Student: **Cascade** (delete student → delete enrollments)
- Enrollment → Course: **Cascade** (delete course → delete enrollments)
- Student & Course → Department: **SetNull** (delete department → set `DepartmentId` to `NULL` on children)

> Service-level safeguards: `DepartmentService.DeleteAsync` prevents deleting a department if there are assigned students or courses. Consider using `Restrict` (DB-level) if you want to enforce the same rule at the DB level.

---

## Common checks / verification SQL
- Find orphan enrollments (missing student/course):
```sql
SELECT e.* FROM Enrollments e
LEFT JOIN Students s ON e.StudentId = s.Id
LEFT JOIN Courses c ON e.CourseId = c.Id
WHERE s.Id IS NULL OR c.Id IS NULL;
```

- Duplicate department names:
```sql
SELECT Name, COUNT(*) cnt FROM Departments GROUP BY Name HAVING COUNT(*) > 1;
```

- Indexes and delete behavior (DB-level):
```sql
SELECT fk.name AS FKName,
       OBJECT_NAME(fk.parent_object_id) ParentTable,
       OBJECT_NAME(fk.referenced_object_id) ReferencedTable,
       fk.delete_referential_action_desc AS OnDelete
FROM sys.foreign_keys fk;
```

---

## Notes & recommendations
- Add a unique index on `Students.Email` if emails must be unique.
- Consider adding a `CHECK` constraint for `NumericGrade` to ensure it lies within the expected range (e.g., 0–100).
- If you want DB-level protection against department deletes, change `SetNull` to `Restrict` and add a migration — this aligns DB and service semantics.

---

*This document is intended as a developer-friendly summary to quickly orient team members and reviewers.*