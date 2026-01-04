using SchoolManagement.Api.Models;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<PagedResult<Student>> GetPagedAsync(StudentQueryParameters parameters);
    Task<PagedResult<Student>> GetByDepartmentPagedAsync(int departmentId, StudentQueryParameters parameters);
    Task<List<(Student student, decimal avgGrade)>> GetTopPerformersAsync(int take = 10);
    Task<bool> DepartmentExistsAsync(int departmentId);
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByIdWithEnrollmentsAsync(int id);
    Task<Student?> GetByEmailAsync(string email);
    Task AddAsync(Student student);
    void Update(Student student);
    void Delete(Student student);
    Task<bool> ExistsAsync(int id);
    Task SaveAsync();
}
