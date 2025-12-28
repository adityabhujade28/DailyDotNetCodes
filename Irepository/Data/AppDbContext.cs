using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Models;
using System.Collections.Generic;

namespace StudentCourseManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
}
