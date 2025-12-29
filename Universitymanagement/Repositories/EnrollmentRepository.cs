using Microsoft.EntityFrameworkCore;
using UniversityManagement.Data;
using UniversityManagement.Interfaces;
using UniversityManagement.Models;

namespace UniversityManagement.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Exists(int studentId, int courseId)
            => await _context.Enrollments
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);

        public async Task EnrollStudent(int studentId, int courseId)
        {
            _context.Enrollments.Add(new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            });
        }

        public async Task DropCourse(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .FindAsync(studentId, courseId);

            if (enrollment != null)
                _context.Enrollments.Remove(enrollment);
        }

        public async Task<List<Course>> GetStudentCourses(int studentId)
            => await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.Course!)
                .ToListAsync();

        public async Task<List<Student>> GetCourseStudents(int courseId)
            => await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.Student!)
                .ToListAsync();
    }
}
