using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentCourseEnrollmentSystem.Data;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Services;
using StudentCourseEnrollmentSystem.Views;

var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        "Server=.;Database=StudentCourseEnrollmentDB;Trusted_Connection=True;TrustServerCertificate=True"
    ));

// Services
services.AddScoped<IStudentService, StudentService>();
services.AddScoped<ICourseService, CourseService>();
services.AddScoped<IEnrollmentService, EnrollmentService>();

// Views
services.AddScoped<StudentView>();
services.AddScoped<CourseView>();
services.AddScoped<EnrollmentView>();
services.AddScoped<MenuView>();

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(dbContext);
}

var menu = serviceProvider.GetRequiredService<MenuView>();
menu.Start();
