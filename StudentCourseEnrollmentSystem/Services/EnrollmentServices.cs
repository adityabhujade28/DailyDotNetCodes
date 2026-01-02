using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentCourseEnrollmentSystem.DTOs;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<EnrollmentService> _logger;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            ILogger<EnrollmentService> logger)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            EnrollStudent(studentId, courseId, null);
        }

        public void EnrollStudent(int studentId, int courseId, decimal? grade)
        {
            // Validate Course
            var course = _courseRepository.GetById(courseId);
            if (course == null)
                throw new Exception("Course not found");

            // Validate Student
            var student = _studentRepository.GetById(studentId);
            if (student == null)
                throw new Exception("Student not found");

            var alreadyEnrolled = _enrollmentRepository.GetByStudentAndCourse(studentId, courseId) != null;

            if (alreadyEnrolled)
                throw new Exception("Student already enrolled in this course");

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = grade,
                EnrolledOn = DateTime.Now,
                Status = EnrollmentStatus.Active
            };

            _enrollmentRepository.Add(enrollment);
            _logger.LogInformation("Student {studentId} enrolled in course {courseId}", studentId, courseId);
        }

        public IEnumerable<EnrollmentDto> GetEnrollments()
        {
            return _enrollmentRepository.GetAll()
                .Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student?.StudentName ?? string.Empty,
                    CourseId = e.CourseId,
                    CourseName = e.Course?.CourseName ?? string.Empty,
                    Grade = e.Grade,
                    EnrolledOn = e.EnrolledOn,
                    Status = e.Status.ToString()
                })
                .OrderBy(e => e.StudentName)
                .ToList();
        }

        public IEnumerable<EnrollmentDto> GetEnrollmentsByStudent(int studentId)
        {
            return _enrollmentRepository.GetByStudent(studentId)
                .Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    CourseName = e.Course?.CourseName ?? string.Empty,
                    Grade = e.Grade,
                    EnrolledOn = e.EnrolledOn,
                    Status = e.Status.ToString()
                })
                .ToList();
        }

        public IEnumerable<EnrollmentDto> GetEnrollmentsByCourse(int courseId)
        {
            return _enrollmentRepository.GetByCourse(courseId)
                .Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student?.StudentName ?? string.Empty,
                    CourseId = e.CourseId,
                    Grade = e.Grade,
                    EnrolledOn = e.EnrolledOn,
                    Status = e.Status.ToString()
                })
                .ToList();
        }

        public void UnenrollStudent(int studentId, int courseId)
        {
            var enrollment = _enrollmentRepository.GetByStudentAndCourse(studentId, courseId);
            if (enrollment == null)
                throw new Exception("Enrollment not found");

            // Soft-drop by updating status
            enrollment.Status = EnrollmentStatus.Dropped;
            _enrollmentRepository.Update(enrollment);
            _logger.LogInformation("Student {studentId} unenrolled from course {courseId}", studentId, courseId);
        }
    }
}
