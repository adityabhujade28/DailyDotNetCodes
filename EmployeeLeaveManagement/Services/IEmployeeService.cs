using EmployeeLeaveManagement.DTOs;

namespace EmployeeLeaveManagement.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeLeaveSummaryDto GetEmployeeLeaveSummary(int employeeId);

        void AddEmployee(string fullName, DateTime joinedOn, int departmentId);
    }
}
