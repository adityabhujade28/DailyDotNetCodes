using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByIdWithEnrollmentsAsync(int id);
    Task<Student?> GetByEmailAsync(string email);
    Task AddAsync(Student student);
    void Update(Student student);
    void Delete(Student student);
    Task<bool> ExistsAsync(int id);
    Task SaveAsync();
}
