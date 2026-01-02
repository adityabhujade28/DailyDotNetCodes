using StudentCourseEnrollmentSystem.Data;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;

namespace StudentCourseEnrollmentSystem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.OrderBy(s => s.StudentName).ToList();
        }

        public IEnumerable<Student> GetAllPaged(int page, int pageSize)
        {
            return _context.Students
                .OrderBy(s => s.StudentName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Student> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Enumerable.Empty<Student>();
            query = query.ToLower();
            return _context.Students
                .Where(s => s.StudentName.ToLower().Contains(query) || s.Email.ToLower().Contains(query))
                .OrderBy(s => s.StudentName)
                .ToList();
        }

        public Student? GetById(int studentId)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}