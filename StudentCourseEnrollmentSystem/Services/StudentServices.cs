using Microsoft.Extensions.Logging;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;
using StudentCourseEnrollmentSystem.DTOs;

namespace StudentCourseEnrollmentSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
        }

        public void AddStudent(string name, string email)
        {
            var student = new Student
            {
                StudentName = name,
                Email = email
            };

            _studentRepository.Add(student);
            _logger.LogInformation("Student added: {name} (ID: {id})", name, student.StudentId);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public Student? GetStudentById(int studentId)
        {
            return _studentRepository.GetById(studentId);
        }

        public void UpdateStudent(int studentId, string name, string email)
        {
            var student = _studentRepository.GetById(studentId);
            if (student == null) throw new Exception("Student not found");

            student.StudentName = name;
            student.Email = email;

            _studentRepository.Update(student);
            _logger.LogInformation("Student updated: {id}", studentId);
        }

        public void DeleteStudent(int studentId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student == null) throw new Exception("Student not found");

            _studentRepository.Delete(student);
            _logger.LogInformation("Student deleted: {id}", studentId);
        }

        public IEnumerable<StudentPerformanceDto> GetTopPerformers(int topN)
        {
            var performances = _enrollmentRepository.GetAll()
                .Where(e => e.Grade.HasValue && e.Status == EnrollmentStatus.Active && e.Student != null)
                .GroupBy(e => new { e.StudentId, e.Student!.StudentName })
                .Select(g => new StudentPerformanceDto
                {
                    StudentId = g.Key.StudentId,
                    StudentName = g.Key.StudentName,
                    AverageGrade = Math.Round(g.Average(x => x.Grade!.Value), 2),
                    CourseCount = g.Count()
                })
                .OrderByDescending(s => s.AverageGrade)
                .Take(topN)
                .ToList();

            return performances;
        }

        public IEnumerable<Student> GetStudentsPaged(int page, int pageSize)
        {
            return _studentRepository.GetAllPaged(page, pageSize);
        }

        public IEnumerable<Student> SearchStudents(string query)
        {
            return _studentRepository.Search(query);
        }
    }
}
