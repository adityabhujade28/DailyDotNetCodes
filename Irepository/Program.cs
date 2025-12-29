using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentCourseManagement.Data;
using StudentCourseManagement.Data.Repositories;
using StudentCourseManagement.Interfaces;
using StudentCourseManagement.Services;
using StudentCourseManagement.Views;
using System.Linq;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure());

    // Helpful for development diagnostics — remove or disable in production
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<StudentService>();
services.AddScoped<StudentView>();

var provider = services.BuildServiceProvider();

// Apply any pending migrations at startup so the database and tables are created automatically.
using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

var view = provider.GetRequiredService<StudentView>();
var studentService = provider.GetRequiredService<StudentService>();

// Interactive console menu
while (true)
{
    Console.WriteLine();
    Console.WriteLine("Student Management - Choose an option:");
    Console.WriteLine("1) List active students");
    Console.WriteLine("2) List all students");
    Console.WriteLine("3) Add a student");
    Console.WriteLine("4) Update a student");
    Console.WriteLine("5) Delete a student");
    Console.WriteLine("6) Exit");
    Console.Write("> ");
    var input = Console.ReadLine()?.Trim();

    if (input == "1")
    {
        await view.ShowActiveStudentsAsync();
    }
    else if (input == "2")
    {
        var all = (await studentService.GetAllStudentsAsync()).ToList();
        if (!all.Any())
        {
            Console.WriteLine("No students found.");
        }
        else
        {
            foreach (var s in all)
            {
                Console.WriteLine($"{s.Id} - {s.Name} {(s.IsActive ? "(Active)" : "(Inactive)")}");
            }
        }
    }
    else if (input == "3")
    {
        Console.Write("Enter student name: ");
        var name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            continue;
        }

        Console.Write("Is the student active? (y/n): ");
        var activeInput = Console.ReadLine()?.Trim().ToLowerInvariant();
        var isActive = activeInput == "y" || activeInput == "yes";

        var student = new StudentCourseManagement.Models.Student { Name = name, IsActive = isActive };
        await studentService.AddStudentAsync(student);
        Console.WriteLine("Student added.");
    }
    else if (input == "4")
    {
        Console.Write("Enter student id to update: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid id.");
            continue;
        }

        var existing = await studentService.GetByIdAsync(id);
        if (existing == null)
        {
            Console.WriteLine($"Student with id {id} not found.");
            continue;
        }

        Console.WriteLine($"Current name: {existing.Name}");
        Console.Write("New name (leave blank to keep): ");
        var newName = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(newName)) existing.Name = newName;

        Console.WriteLine($"Current status: {(existing.IsActive ? "Active" : "Inactive")}");
        Console.Write("Is active? (y/n, leave blank to keep): ");
        var activeInput = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (activeInput == "y" || activeInput == "yes") existing.IsActive = true;
        else if (activeInput == "n" || activeInput == "no") existing.IsActive = false;

        await studentService.UpdateStudentAsync(existing);
        Console.WriteLine("Student updated.");
    }
    else if (input == "5")
    {
        Console.Write("Enter student id to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var idToDelete))
        {
            Console.WriteLine("Invalid id.");
            continue;
        }

        var student = await studentService.GetByIdAsync(idToDelete);
        if (student == null)
        {
            Console.WriteLine($"Student with id {idToDelete} not found.");
            continue;
        }

        Console.Write($"Are you sure you want to delete '{student.Name}'? (y/N): ");
        var confirm = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (confirm != "y" && confirm != "yes")
        {
            Console.WriteLine("Delete cancelled.");
            continue;
        }

        var deleted = await studentService.DeleteStudentAsync(idToDelete);
        Console.WriteLine(deleted ? "Student deleted." : "Delete failed.");
    }
    else if (input == "6" || input?.Equals("exit", StringComparison.OrdinalIgnoreCase) == true)
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid option. Please choose 1-6.");
    }
}

