using EmployeeLeaveManagement.Data;
using EmployeeLeaveManagement.Services;
using EmployeeLeaveManagement.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Database
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=.;Database=EmployeeLeaveManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
    )
);

services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<ILeaveService, LeaveService>();

// Views
services.AddScoped<EmployeeView>();
services.AddScoped<LeaveView>();
services.AddScoped<DashboardView>();
services.AddScoped<AdminView>();

var serviceProvider = services.BuildServiceProvider();

// Database Initialization
try
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    DbSeeder.Seed(context);
}
catch (Exception ex)
{
    Console.WriteLine("Database initialization failed: " + ex.Message);
    Console.WriteLine(ex.ToString());
}

try
{
    using var scope = serviceProvider.CreateScope();
    var employeeView = scope.ServiceProvider.GetRequiredService<EmployeeView>();
    var leaveView = scope.ServiceProvider.GetRequiredService<LeaveView>();
    var dashboardView = scope.ServiceProvider.GetRequiredService<DashboardView>();
    var adminView = scope.ServiceProvider.GetRequiredService<AdminView>();

    bool exit = false;

    while (!exit)
    {
        Console.WriteLine("1. View All Employees");
        Console.WriteLine("2. Add Employee");
        Console.WriteLine("3. View All Leave Requests");
        Console.WriteLine("4. Add Leave Request");
        Console.WriteLine("5. View Employee Leave Summary");
        Console.WriteLine("6. Exit");
        Console.WriteLine("7. Admin - View Pending Leaves");
        Console.WriteLine("8. Admin - Approve / Reject Leave");

        var choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                employeeView.ShowAllEmployees();
                break;

            case "2":
                employeeView.AddEmployee();
                break;

            case "3":
                leaveView.ShowAllLeaveRequests();
                break;

            case "4":
                leaveView.AddLeaveRequest();
                break;

            case "5":
                dashboardView.ShowEmployeeLeaveSummary();
                break;

            case "6":
                exit = true;
                break;

            case "7":
                adminView.ShowPendingLeaves();
                break;

            case "8":
                adminView.ApproveOrRejectLeave();
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }

        Console.WriteLine();
    }
}
catch (Exception innerEx)
{
    Console.WriteLine("Application error: " + innerEx.Message);
    Console.WriteLine(innerEx.ToString());
}
finally
{
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
}