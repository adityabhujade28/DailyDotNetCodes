using StudentCourseEnrollmentSystem.Data;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddStudent(string name, string email)
        {
            var student = new Student
            {
                StudentName = name,
                Email = email
            };

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students
                           .OrderBy(s => s.StudentName)
                           .ToList();
        }

        public Student? GetStudentById(int studentId)
        {
            return _context.Students
                           .FirstOrDefault(s => s.StudentId == studentId);
        }
    }
}
