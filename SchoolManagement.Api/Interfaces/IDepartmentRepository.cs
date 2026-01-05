using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Interfaces;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task<Department?> GetByNameAsync(string name);
    Task AddAsync(Department department);
    void Update(Department department);
    void Delete(Department department);
    Task<bool> ExistsAsync(int id);
    Task<bool> HasStudentsAsync(int departmentId);
    Task<bool> HasCoursesAsync(int departmentId);
    Task SaveAsync();
}