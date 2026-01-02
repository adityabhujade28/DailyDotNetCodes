using StudentCourseEnrollmentSystem.Data;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.OrderBy(c => c.CourseName).ToList();
        }

        public IEnumerable<Course> GetAllPaged(int page, int pageSize)
        {
            return _context.Courses
                .OrderBy(c => c.CourseName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Course> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Enumerable.Empty<Course>();
            query = query.ToLower();
            return _context.Courses
                .Where(c => c.CourseName.ToLower().Contains(query))
                .OrderBy(c => c.CourseName)
                .ToList();
        }

        public Course? GetById(int courseId)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}