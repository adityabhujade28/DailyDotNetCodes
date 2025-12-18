using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface IStudentService
    {
        void AddStudent(string name, string email);

        IEnumerable<Student> GetAllStudents();

        Student? GetStudentById(int studentId);
    }
}
