using EmployeeLeaveManagement.Services;

namespace EmployeeLeaveManagement.Views
{
    public class DashboardView
    {
        private readonly IEmployeeService _employeeService;

        public DashboardView(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void ShowEmployeeLeaveSummary()
        {
            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine()!);

            var summary = _employeeService.GetEmployeeLeaveSummary(employeeId);

            Console.WriteLine("\nLeave Summary");
            Console.WriteLine($"Employee: {summary.EmployeeName}");
            Console.WriteLine($"Total Leaves: {summary.TotalLeavesTaken}");
            Console.WriteLine($"Approved: {summary.ApprovedLeaves}");
            Console.WriteLine($"Pending: {summary.PendingLeaves}");
            Console.WriteLine();
        }
    }
}
