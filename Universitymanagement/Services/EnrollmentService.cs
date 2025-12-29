using UniversityManagement.Data;
using UniversityManagement.Interfaces;
using UniversityManagement.Models;

namespace UniversityManagement.Services
{
    public class EnrollmentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly AppDbContext _context;

        public EnrollmentService(
            IStudentRepository studentRepo,
            ICourseRepository courseRepo,
            IEnrollmentRepository enrollmentRepo,
            AppDbContext context)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
            _enrollmentRepo = enrollmentRepo;
            _context = context;
        }

        public async Task EnrollStudent(int studentId, int courseId)
        {
            var student = await _studentRepo.GetById(studentId)
                ?? throw new Exception("Student not found");

            var course = await _courseRepo.GetById(courseId)
                ?? throw new Exception("Course not found");

            if (!await _courseRepo.HasCapacity(courseId))
                throw new Exception("Course is full");

            if (await _enrollmentRepo.Exists(studentId, courseId))
                throw new Exception("Student already enrolled");

            await _enrollmentRepo.EnrollStudent(studentId, courseId);
            course.CurrentCapacity++;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>> GetStudentCourses(int studentId)
        {
            return await _enrollmentRepo.GetStudentCourses(studentId);
        }
    }
}
