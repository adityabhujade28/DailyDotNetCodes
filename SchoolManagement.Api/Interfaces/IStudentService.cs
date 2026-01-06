using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface IStudentService
{
    Task<List<StudentResponseDto>> GetAllAsync();
    Task<PagedResult<StudentResponseDto>> GetPagedAsync(StudentQueryParameters parameters);
    Task<PagedResult<StudentResponseDto>> GetByDepartmentAsync(int departmentId, StudentQueryParameters parameters);
    Task<List<StudentPerformanceDto>> GetTopPerformersAsync(int take = 10);
    Task<StudentResponseDto?> GetByIdAsync(int id);
    Task<StudentDetailDto?> GetByIdWithEnrollmentsAsync(int id);
    Task<StudentResponseDto> CreateAsync(StudentCreateDto dto);
    Task<StudentResponseDto?> UpdateAsync(int id, StudentUpdateDto dto);
    Task<StudentResponseDto?> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
