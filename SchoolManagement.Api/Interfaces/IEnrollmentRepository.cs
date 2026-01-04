using SchoolManagement.Api.Models;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdAsync(int id);
    Task<PagedResult<Enrollment>> GetPagedAsync(EnrollmentQueryParameters parameters);
    Task<List<Enrollment>> GetByStudentIdAsync(int studentId);
    Task<List<Enrollment>> GetByCourseIdAsync(int courseId);
    Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId);
    Task AddAsync(Enrollment enrollment);
    void Update(Enrollment enrollment);
    void Delete(Enrollment enrollment);
    Task SaveAsync();
}