using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface ICourseService
    {
        void AddCourse(string courseName, int credits);

        IEnumerable<Course> GetAllCourses();

        Course? GetCourseById(int courseId);
    }
}
