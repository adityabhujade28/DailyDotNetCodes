using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface IEnrollmentRepository
    {
        void Add(Enrollment enrollment);
        IEnumerable<Enrollment> GetAll();
        Enrollment? GetById(int enrollmentId);
        Enrollment? GetByStudentAndCourse(int studentId, int courseId);
        IEnumerable<Enrollment> GetByStudent(int studentId);
        IEnumerable<Enrollment> GetByCourse(int courseId);
        void Update(Enrollment enrollment);
        void Delete(Enrollment enrollment);
    }
}