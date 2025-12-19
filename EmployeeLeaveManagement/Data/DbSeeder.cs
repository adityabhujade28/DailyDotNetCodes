using EmployeeLeaveManagement.Models;

namespace EmployeeLeaveManagement.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Departments.Any())
                return;


            var it = new Department { Name = "IT" };
            var hr = new Department { Name = "HR" };

            context.Departments.AddRange(it, hr);

            var emp1 = new Employee
            {
                FullName = "Amit Sharma",
                JoinedOn = DateTime.Now.AddYears(-3),
                Department = it
            };

            var emp2 = new Employee
            {
                FullName = "Neha Verma",
                JoinedOn = DateTime.Now.AddYears(-2),
                Department = hr
            };

            context.Employees.AddRange(emp1, emp2);

            context.LeaveRequests.AddRange(
                new LeaveRequest
                {
                    Employee = emp1,
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-7),
                    LeaveType = "Sick",
                    Status = "Approved"
                },
                new LeaveRequest
                {
                    Employee = emp1,
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(-3),
                    LeaveType = "Casual",
                    Status = "Approved"
                },
                new LeaveRequest
                {
                    Employee = emp2,
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now.AddDays(-6),
                    LeaveType = "Casual",
                    Status = "Pending"
                }
            );

            context.SaveChanges();
        }
    }
}
