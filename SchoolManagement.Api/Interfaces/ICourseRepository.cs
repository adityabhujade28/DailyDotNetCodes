using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<List<Course>> GetByDepartmentAsync(int departmentId);
    Task<Course?> GetByIdAsync(int id);
    Task<Course?> GetByTitleAsync(string title);
    Task AddAsync(Course course);
    void Update(Course course);
    void Delete(Course course);
    Task<bool> ExistsAsync(int id);
    Task SaveAsync();
}