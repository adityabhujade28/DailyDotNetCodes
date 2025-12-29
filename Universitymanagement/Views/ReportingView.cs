using UniversityManagement.Services;

namespace UniversityManagement.Views
{
    public class ReportingView
    {
        private readonly ReportingService _reportingService;

        public ReportingView(ReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        public async Task DepartmentReport()
        {
            Console.Write("Enter Department Id: ");
            if (!int.TryParse(Console.ReadLine(), out var deptId))
            {
                Console.WriteLine("Invalid Department Id");
                return;
            }

            var report = await _reportingService.GetDepartmentReport(deptId);
            if (report == null)
            {
                Console.WriteLine("Department not found");
                return;
            }

            Console.WriteLine("\n--- Department Report ---");
            Console.WriteLine($"Total Students: {report.TotalStudents}");
            Console.WriteLine($"Average GPA: {report.AverageGpa:F2}");
        }
    }
}
