using EmployeeLeaveManagement.Data;
using EmployeeLeaveManagement.DTOs;
using EmployeeLeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FullName = e.FullName,
                    JoinedOn = e.JoinedOn,
                    DepartmentName = e.Department.Name
                })
                .ToList();
        }

        public EmployeeLeaveSummaryDto GetEmployeeLeaveSummary(int employeeId)
        {
            var employee = _context.Employees
                .Include(e => e.LeaveRequests)
                .First(e => e.EmployeeId == employeeId);

            return new EmployeeLeaveSummaryDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.FullName,
                TotalLeavesTaken = employee.LeaveRequests.Count,
                ApprovedLeaves = employee.LeaveRequests.Count(l => l.Status == "Approved"),
                PendingLeaves = employee.LeaveRequests.Count(l => l.Status == "Pending")
            };
        }

        public void AddEmployee(string fullName, DateTime joinedOn, int departmentId)
        {
            var employee = new Employee
            {
                FullName = fullName,
                JoinedOn = joinedOn,
                DepartmentId = departmentId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
    }
}
