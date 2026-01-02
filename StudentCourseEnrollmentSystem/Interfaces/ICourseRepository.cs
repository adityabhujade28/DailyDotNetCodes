using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        IEnumerable<Course> GetAll();
        IEnumerable<Course> GetAllPaged(int page, int pageSize);
        IEnumerable<Course> Search(string query);
        Course? GetById(int courseId);
        void Update(Course course);
        void Delete(Course course);
    }
}