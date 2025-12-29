using UniversityManagement.Models;

namespace UniversityManagement.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<bool> Exists(int studentId, int courseId);
        Task EnrollStudent(int studentId, int courseId);
        Task DropCourse(int studentId, int courseId);
        Task<List<Course>> GetStudentCourses(int studentId);
        Task<List<Student>> GetCourseStudents(int courseId);
    }
}
