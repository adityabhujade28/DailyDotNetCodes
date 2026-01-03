using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface IStudentService
{
    Task<List<StudentResponseDto>> GetAllAsync();
    Task<StudentResponseDto?> GetByIdAsync(int id);
    Task<StudentDetailDto?> GetByIdWithEnrollmentsAsync(int id);
    Task<StudentResponseDto> CreateAsync(StudentCreateDto dto);
    Task<StudentResponseDto?> UpdateAsync(int id, StudentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
