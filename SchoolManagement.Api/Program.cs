using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Mappings;
using SchoolManagement.Api.Repositories;
using SchoolManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register Mapster mappings
MappingConfig.RegisterMappings();
builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Correlation ID and request logging
app.UseMiddleware<SchoolManagement.Api.Middleware.CorrelationIdMiddleware>();
app.UseMiddleware<SchoolManagement.Api.Middleware.RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply pending EF Core migrations at startup (creates DB if missing)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    // Seed sample data for Departments and Courses if empty
    // Seed engineering-focused Departments
    if (!db.Departments.Any())
    {
        db.Departments.AddRange(new[] {
            new SchoolManagement.Api.Models.Department { Name = "Computer Science" },
            new SchoolManagement.Api.Models.Department { Name = "Electrical Engineering" },
            new SchoolManagement.Api.Models.Department { Name = "Mechanical Engineering" }
        });
        db.SaveChanges();
    }

    // Seed engineering Courses
    if (!db.Courses.Any())
    {
        var csId = db.Departments.First(d => d.Name == "Computer Science").Id;
        var eeId = db.Departments.First(d => d.Name == "Electrical Engineering").Id;
        var meId = db.Departments.First(d => d.Name == "Mechanical Engineering").Id;

        db.Courses.AddRange(new[] {
            new SchoolManagement.Api.Models.Course { Title = "Intro to Programming", Credits = 4, Description = "Fundamentals of programming (C#)", DepartmentId = csId },
            new SchoolManagement.Api.Models.Course { Title = "Data Structures", Credits = 4, Description = "Data structures and algorithms", DepartmentId = csId },
            new SchoolManagement.Api.Models.Course { Title = "Circuits 101", Credits = 3, Description = "Basics of electrical circuits", DepartmentId = eeId },
            new SchoolManagement.Api.Models.Course { Title = "Digital Systems", Credits = 3, Description = "Digital logic and design", DepartmentId = eeId },
            new SchoolManagement.Api.Models.Course { Title = "Statics", Credits = 3, Description = "Engineering statics", DepartmentId = meId },
            new SchoolManagement.Api.Models.Course { Title = "Thermodynamics", Credits = 3, Description = "Intro to thermodynamics", DepartmentId = meId }
        });
        db.SaveChanges();
    }

    // Seed students (at least 10) for engineering departments
    if (!db.Students.Any())
    {
        var csId = db.Departments.First(d => d.Name == "Computer Science").Id;
        var eeId = db.Departments.First(d => d.Name == "Electrical Engineering").Id;
        var meId = db.Departments.First(d => d.Name == "Mechanical Engineering").Id;

        db.Students.AddRange(new[] {
            new SchoolManagement.Api.Models.Student { Name = "Alice Johnson", Email = "alice.johnson@eng.edu", PhoneNumber = "555-0101", DateOfBirth = new DateTime(2002,5,12), Address = "City A", DepartmentId = csId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Bob Smith", Email = "bob.smith@eng.edu", PhoneNumber = "555-0102", DateOfBirth = new DateTime(2001,8,3), Address = "City B", DepartmentId = csId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Cathy Lee", Email = "cathy.lee@eng.edu", PhoneNumber = "555-0103", DateOfBirth = new DateTime(2003,2,22), Address = "City C", DepartmentId = eeId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "David Kim", Email = "david.kim@eng.edu", PhoneNumber = "555-0104", DateOfBirth = new DateTime(2002,11,5), Address = "City D", DepartmentId = eeId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Eva Brown", Email = "eva.brown@eng.edu", PhoneNumber = "555-0105", DateOfBirth = new DateTime(2001,7,19), Address = "City E", DepartmentId = meId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Frank Green", Email = "frank.green@eng.edu", PhoneNumber = "555-0106", DateOfBirth = new DateTime(2000,3,8), Address = "City F", DepartmentId = meId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Grace Hall", Email = "grace.hall@eng.edu", PhoneNumber = "555-0107", DateOfBirth = new DateTime(2003,9,30), Address = "City G", DepartmentId = csId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Henry Young", Email = "henry.young@eng.edu", PhoneNumber = "555-0108", DateOfBirth = new DateTime(2002,1,14), Address = "City H", DepartmentId = eeId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Irene Scott", Email = "irene.scott@eng.edu", PhoneNumber = "555-0109", DateOfBirth = new DateTime(2001,4,26), Address = "City I", DepartmentId = meId, CreatedAt = DateTime.UtcNow },
            new SchoolManagement.Api.Models.Student { Name = "Jack Turner", Email = "jack.turner@eng.edu", PhoneNumber = "555-0110", DateOfBirth = new DateTime(2000,12,2), Address = "City J", DepartmentId = csId, CreatedAt = DateTime.UtcNow }
        });
        db.SaveChanges();
    }

    // Seed enrollments linking students to courses
    if (!db.Enrollments.Any())
    {
        var rnd = new Random(42);
        var students = db.Students.ToList();
        var courses = db.Courses.ToList();
        var enrollments = new List<SchoolManagement.Api.Models.Enrollment>();

        // Ensure each student has at least one enrollment and some have multiple
        foreach (var s in students)
        {
            // enroll in 1 to 3 random courses
            var take = 1 + (rnd.Next() % 3);
            var chosen = courses.OrderBy(c => rnd.Next()).Take(take).ToList();
            foreach (var c in chosen)
            {
                enrollments.Add(new SchoolManagement.Api.Models.Enrollment
                {
                    StudentId = s.Id,
                    CourseId = c.Id,
                    EnrollmentDate = DateTime.UtcNow.AddDays(-rnd.Next(0, 365)),
                    Grade = rnd.Next(0, 2) == 0 ? "A" : "B",
                    NumericGrade = Math.Round((decimal)(50 + rnd.NextDouble() * 50), 2)
                });
            }
        }

        db.Enrollments.AddRange(enrollments);
        db.SaveChanges();
    }
}

app.Run();
