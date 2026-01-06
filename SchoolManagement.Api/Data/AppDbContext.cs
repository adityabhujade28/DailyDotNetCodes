using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Enrollment relationships
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Department relationship
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Department)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Numeric grade precision
        modelBuilder.Entity<Enrollment>()
            .Property(e => e.NumericGrade)
            .HasPrecision(5, 2);

        // Add unique constraint for student-course enrollment
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.CourseId })
            .IsUnique();

        // Ensure course titles are unique
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Title)
            .IsUnique();

        // Ensure department names are unique
        modelBuilder.Entity<Department>()
            .HasIndex(d => d.Name)
            .IsUnique();

        // Course -> Department relationship
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data: Engineering department, courses, students and enrollments
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Engineering" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Introduction to Computer Engineering", Description = "Fundamentals of computer engineering", Credits = 3, DepartmentId = 1 },
            new Course { Id = 2, Title = "Data Structures and Algorithms", Description = "Core data structures and algorithm analysis", Credits = 4, DepartmentId = 1 },
            new Course { Id = 3, Title = "Digital Logic Design", Description = "Combinational and sequential circuits", Credits = 3, DepartmentId = 1 },
            new Course { Id = 4, Title = "Signals and Systems", Description = "Continuous and discrete signal analysis", Credits = 3, DepartmentId = 1 },
            new Course { Id = 5, Title = "Computer Networks", Description = "Networking principles and protocols", Credits = 3, DepartmentId = 1 }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Aarav Sharma", Email = "aarav.sharma@eng.example.edu", DateOfBirth = new DateTime(2002, 5, 10), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 2, Name = "Priya Verma", Email = "priya.verma@eng.example.edu", DateOfBirth = new DateTime(2001, 3, 22), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 3, Name = "Rohit Gupta", Email = "rohit.gupta@eng.example.edu", DateOfBirth = new DateTime(2002, 11, 5), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 4, Name = "Neha Singh", Email = "neha.singh@eng.example.edu", DateOfBirth = new DateTime(2001, 7, 14), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 5, Name = "Vikram Patel", Email = "vikram.patel@eng.example.edu", DateOfBirth = new DateTime(2003, 2, 28), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 6, Name = "Sagar Kulkarni", Email = "sagar.kulkarni@eng.example.edu", DateOfBirth = new DateTime(2002, 9, 3), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 7, Name = "Sneha Reddy", Email = "sneha.reddy@eng.example.edu", DateOfBirth = new DateTime(2001, 12, 30), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 8, Name = "Manish Kumar", Email = "manish.kumar@eng.example.edu", DateOfBirth = new DateTime(2002, 4, 17), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 9, Name = "Anika Kapoor", Email = "anika.kapoor@eng.example.edu", DateOfBirth = new DateTime(2003, 6, 9), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 },
            new Student { Id = 10, Name = "Karan Mehta", Email = "karan.mehta@eng.example.edu", DateOfBirth = new DateTime(2001, 1, 20), CreatedAt = new DateTime(2024, 8, 1), DepartmentId = 1 }
        );

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A", NumericGrade = 4.00m },
            new Enrollment { Id = 2, StudentId = 1, CourseId = 2, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A-", NumericGrade = 3.70m },
            new Enrollment { Id = 3, StudentId = 2, CourseId = 2, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B+", NumericGrade = 3.30m },
            new Enrollment { Id = 4, StudentId = 3, CourseId = 3, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B", NumericGrade = 3.00m },
            new Enrollment { Id = 5, StudentId = 4, CourseId = 4, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A", NumericGrade = 4.00m },
            new Enrollment { Id = 6, StudentId = 5, CourseId = 5, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B-", NumericGrade = 2.70m },
            new Enrollment { Id = 7, StudentId = 6, CourseId = 1, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A", NumericGrade = 4.00m },
            new Enrollment { Id = 8, StudentId = 7, CourseId = 2, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B+", NumericGrade = 3.30m },
            new Enrollment { Id = 9, StudentId = 8, CourseId = 3, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A-", NumericGrade = 3.70m },
            new Enrollment { Id = 10, StudentId = 9, CourseId = 4, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "A", NumericGrade = 4.00m },
            new Enrollment { Id = 11, StudentId = 10, CourseId = 5, EnrollmentDate = new DateTime(2024, 9, 1), Grade = "B", NumericGrade = 3.00m }
        );

    }
}
