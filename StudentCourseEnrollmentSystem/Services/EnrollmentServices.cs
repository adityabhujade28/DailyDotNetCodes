using Microsoft.EntityFrameworkCore;
using StudentCourseEnrollmentSystem.Data;
using StudentCourseEnrollmentSystem.DTOs;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

       

        public void EnrollStudent(int studentId, int courseId)
        {
            EnrollStudent(studentId, courseId, null);
        }

        public void EnrollStudent(int studentId, int courseId, decimal? grade)
        {
            // Validate Student
            var studentExists = _context.Students.Any(s => s.StudentId == studentId);
            if (!studentExists)
                throw new Exception("Student not found");

            // Validate Course
            var courseExists = _context.Courses.Any(c => c.CourseId == courseId);
            if (!courseExists)
                throw new Exception("Course not found");

            // Prevent duplicate enrollment (extra safety)
            var alreadyEnrolled = _context.Enrollments
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);

            if (alreadyEnrolled)
                throw new Exception("Student already enrolled in this course");

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = grade,
                EnrolledOn = DateTime.Now,
                Status = "Active"
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }


        public IEnumerable<EnrollmentDto> GetEnrollments()
        {
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student.StudentName,
                    CourseId = e.CourseId,
                    CourseName = e.Course.CourseName,
                    Grade = e.Grade,
                    EnrolledOn = e.EnrolledOn,
                    Status = e.Status
                })
                .OrderBy(e => e.StudentName)
                .ToList();
        }

        public IEnumerable<EnrollmentDto> GetEnrollmentsByStudent(int studentId)
        {
            return _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    CourseName = e.Course.CourseName,
                    Grade = e.Grade,
                    EnrolledOn = e.EnrolledOn,
                    Status = e.Status
                })
                .ToList();
        }

        public IEnumerable<EnrollmentDto> GetEnrollmentsByCourse(int courseId)
        {
            return _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Student)
                .Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student.StudentName,
                    CourseId = e.CourseId,
                    Grade = e.Grade,
                    EnrolledOn = e.EnrolledOn,
                    Status = e.Status
                })
                .ToList();
        }
    }
}
