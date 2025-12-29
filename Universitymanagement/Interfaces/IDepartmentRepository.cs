using UniversityManagement.Models;

namespace UniversityManagement.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetById(int id);
        Task<Department?> GetWithStudents(int id);
        Task Add(Department department);
    }
}
