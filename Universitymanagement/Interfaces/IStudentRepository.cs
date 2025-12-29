using UniversityManagement.Models;

namespace UniversityManagement.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetById(int id);
        Task<List<Student>> GetAll();
        Task<List<Student>> GetByDepartment(int departmentId);
        Task<List<Student>> GetTopPerformers(int count);
        Task<List<Enrollment>> GetEnrollments(int studentId);
        Task Add(Student student);
        Task Update(Student student);
    }
}
