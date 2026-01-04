using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface IEnrollmentService
{
    Task<EnrollmentDto> CreateAsync(EnrollmentCreateDto dto);
    Task<PagedResult<EnrollmentDto>> GetPagedAsync(EnrollmentQueryParameters parameters);
    Task<List<EnrollmentDto>> GetByStudentAsync(int studentId);
    Task<List<EnrollmentDto>> GetByCourseAsync(int courseId);
    Task<EnrollmentDto?> UpdateAsync(int id, EnrollmentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}