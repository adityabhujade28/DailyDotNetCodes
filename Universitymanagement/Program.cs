using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityManagement.Data;
using UniversityManagement.Interfaces;
using UniversityManagement.Repositories;
using UniversityManagement.Services;
using UniversityManagement.Views;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<ICourseRepository, CourseRepository>();
services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
services.AddScoped<IDepartmentRepository, DepartmentRepository>();


services.AddScoped<StudentView>();
services.AddScoped<EnrollmentView>();
services.AddScoped<ReportingView>();
services.AddScoped<CourseView>();
services.AddScoped<DepartmentView>();
services.AddScoped<MainMenuView>();


services.AddScoped<ReportingService>();
services.AddScoped<EnrollmentService>();

var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

Console.WriteLine("Application started successfully");

var menu = provider.GetRequiredService<MainMenuView>();
await menu.ShowMenu();
