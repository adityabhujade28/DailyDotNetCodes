using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<List<CourseDto>> GetByDepartmentAsync(int departmentId);
    Task<CourseDto?> GetByIdAsync(int id);
    Task<CourseDto> CreateAsync(CourseCreateDto dto);
    Task<CourseDto?> UpdateAsync(int id, CourseUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}