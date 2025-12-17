using StudentCourseEnrollmentSystem.DTOs;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface IEnrollmentService
    {
        // Method overloading
        void EnrollStudent(int studentId, int courseId);
        void EnrollStudent(int studentId, int courseId, decimal? grade);

        IEnumerable<EnrollmentDto> GetEnrollments();
        IEnumerable<EnrollmentDto> GetEnrollmentsByStudent(int studentId);
        IEnumerable<EnrollmentDto> GetEnrollmentsByCourse(int courseId);
    }
}
