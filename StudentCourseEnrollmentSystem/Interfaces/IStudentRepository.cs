using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        IEnumerable<Student> GetAll();
        IEnumerable<Student> GetAllPaged(int page, int pageSize);
        IEnumerable<Student> Search(string query);
        Student? GetById(int studentId);
        void Update(Student student);
        void Delete(Student student);
    }
}