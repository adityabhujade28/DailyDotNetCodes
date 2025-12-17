using StudentCourseEnrollmentSystem.Data;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCourse(string courseName, int credits)
        {
            var course = new Course
            {
                CourseName = courseName,
                Credits = credits
            };

            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses
                           .OrderBy(c => c.CourseName)
                           .ToList();
        }

        public Course? GetCourseById(int courseId)
        {
            return _context.Courses
                           .FirstOrDefault(c => c.CourseId == courseId);
        }
    }
}
