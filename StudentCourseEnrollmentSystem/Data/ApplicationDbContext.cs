using Microsoft.EntityFrameworkCore;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.StudentId, e.CourseId })
                .IsUnique();

            // Store EnrollmentStatus enum as string (e.g., "Active", "Dropped")
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            // Configure Grade precision to avoid truncation warnings
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Grade)
                .HasPrecision(3, 2);

            // Audit/soft-delete columns are model properties; configure defaults where helpful
            modelBuilder.Entity<Student>()
                .Property(s => s.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Course>()
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false); 
        }
    }
}
