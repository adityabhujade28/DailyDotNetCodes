using UniversityManagement.Models;

namespace UniversityManagement.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course?> GetById(int id);
        Task<List<Course>> GetAll();
        Task<bool> HasCapacity(int courseId);
        Task<List<Student>> GetStudents(int courseId);
        Task Add(Course course);
    }
}
