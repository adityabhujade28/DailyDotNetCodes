using StudentCourseEnrollmentSystem.Models;
using StudentCourseEnrollmentSystem.DTOs;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface IStudentService
    {
        void AddStudent(string name, string email);

        IEnumerable<Student> GetAllStudents();

        Student? GetStudentById(int studentId);

        void UpdateStudent(int studentId, string name, string email);

        void DeleteStudent(int studentId);

        IEnumerable<StudentPerformanceDto> GetTopPerformers(int topN);

        IEnumerable<Student> GetStudentsPaged(int page, int pageSize);

        IEnumerable<Student> SearchStudents(string query);
    }
}
