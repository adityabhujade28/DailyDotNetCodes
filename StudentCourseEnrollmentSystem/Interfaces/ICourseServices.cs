using StudentCourseEnrollmentSystem.Models;
using StudentCourseEnrollmentSystem.DTOs;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface ICourseService
    {
        void AddCourse(string courseName, int credits);

        IEnumerable<Course> GetAllCourses();

        Course? GetCourseById(int courseId);

        void UpdateCourse(int courseId, string courseName, int credits);

        void DeleteCourse(int courseId);

        IEnumerable<CourseStatsDto> GetCourseStatistics();

        IEnumerable<Course> GetCoursesPaged(int page, int pageSize);

        IEnumerable<Course> SearchCourses(string query);
    }
}
