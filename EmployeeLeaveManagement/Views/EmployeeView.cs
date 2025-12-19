using EmployeeLeaveManagement.Services;

namespace EmployeeLeaveManagement.Views
{
    public class EmployeeView
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeView(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void ShowAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();

            Console.WriteLine(" Employees ");
            foreach (var emp in employees)
            {
                Console.WriteLine(
                    $"{emp.EmployeeId} | {emp.FullName} | {emp.DepartmentName} | Joined: {emp.JoinedOn:d}"
                );
            }
        }

        public void AddEmployee()
        {
            Console.Write("Enter Full Name: ");
            var name = Console.ReadLine()!;

            Console.Write("Enter Department ID (1 = IT, 2 = HR): ");
            int deptId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Joined Date (yyyy-mm-dd): ");
            DateTime joinedOn = DateTime.Parse(Console.ReadLine()!);

            _employeeService.AddEmployee(name, joinedOn, deptId);
            Console.WriteLine("Employee added successfully.");
        }
    }
}
