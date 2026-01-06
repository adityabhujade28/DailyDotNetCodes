using SchoolManagement.Api.Models;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<PagedResult<Course>> GetPagedAsync(CourseQueryParameters parameters);
    Task<List<Course>> GetByDepartmentAsync(int departmentId);
    Task<Course?> GetByIdAsync(int id);
    Task<Course?> GetByTitleAsync(string title);
    Task AddAsync(Course course);
    void Update(Course course);
    void Delete(Course course);
    Task<bool> HasEnrollmentsAsync(int courseId);
    Task<bool> ExistsAsync(int id);
    Task SaveAsync();
}