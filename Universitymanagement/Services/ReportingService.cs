using UniversityManagement.Interfaces;

namespace UniversityManagement.Services
{
    public class ReportingService
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IStudentRepository _studentRepo;

        public ReportingService(
            IDepartmentRepository departmentRepo,
            IStudentRepository studentRepo)
        {
            _departmentRepo = departmentRepo;
            _studentRepo = studentRepo;
        }

        public async Task<DepartmentReport?> GetDepartmentReport(int departmentId)
        {
            var department = await _departmentRepo.GetWithStudents(departmentId);
            if (department == null)
                return null;

            var students = department.Students;

            return new DepartmentReport
            {
                TotalStudents = students.Count,
                AverageGpa = students.Any()
                    ? students.Average(s => s.GPA)
                    : 0
            };
        }
    }

    public class DepartmentReport
    {
        public int TotalStudents { get; set; }
        public double AverageGpa { get; set; }
    }
}
