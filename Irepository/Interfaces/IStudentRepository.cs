using StudentCourseManagement.Models;

namespace StudentCourseManagement.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetActiveStudentsAsync();
}
