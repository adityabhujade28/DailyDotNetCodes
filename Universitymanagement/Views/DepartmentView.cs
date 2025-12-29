using UniversityManagement.Interfaces;

namespace UniversityManagement.Views
{
    public class DepartmentView
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentView(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task AddDepartment()
        {
            Console.Write("Enter Department Name: ");
            var name = Console.ReadLine();

            var department = new UniversityManagement.Models.Department
            {
                Name = name ?? string.Empty
            };

            try
            {
                await _departmentRepository.Add(department);
                Console.WriteLine("Department added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding department: {ex.Message}");
            }
        }
    }
}
