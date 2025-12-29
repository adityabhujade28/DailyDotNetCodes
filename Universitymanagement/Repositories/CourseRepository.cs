using Microsoft.EntityFrameworkCore;
using UniversityManagement.Data;
using UniversityManagement.Interfaces;
using UniversityManagement.Models;

namespace UniversityManagement.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetById(int id)
            => await _context.Courses.FindAsync(id);

        public async Task<List<Course>> GetAll()
            => await _context.Courses.ToListAsync();

        public async Task<bool> HasCapacity(int courseId)
        {
            var course = await GetById(courseId);
            return course != null && course.CurrentCapacity < course.MaxCapacity;
        }

        public async Task<List<Student>> GetStudents(int courseId)
            => await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.Student!)
                .ToListAsync();

        public async Task Add(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
    }
}
