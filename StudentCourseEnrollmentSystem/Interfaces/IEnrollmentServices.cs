using StudentCourseEnrollmentSystem.DTOs;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface IEnrollmentService
    {
        void EnrollStudent(int studentId, int courseId);
        void EnrollStudent(int studentId, int courseId, decimal? grade);
        void UnenrollStudent(int studentId, int courseId);

        IEnumerable<EnrollmentDto> GetEnrollments();
        IEnumerable<EnrollmentDto> GetEnrollmentsByStudent(int studentId);
        IEnumerable<EnrollmentDto> GetEnrollmentsByCourse(int courseId);
    }
}
