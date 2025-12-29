using Microsoft.EntityFrameworkCore;
using UniversityManagement.Data;
using UniversityManagement.Interfaces;
using UniversityManagement.Models;

namespace UniversityManagement.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetById(int id)
            => await _context.Students.FindAsync(id);

        public async Task<List<Student>> GetAll()
            => await _context.Students.ToListAsync();

        public async Task<List<Student>> GetByDepartment(int departmentId)
            => await _context.Students
                .Where(s => s.DepartmentId == departmentId)
                .ToListAsync();

        public async Task<List<Student>> GetTopPerformers(int count)
            => await _context.Students
                .OrderByDescending(s => s.GPA)
                .Take(count)
                .ToListAsync();

        public async Task<List<Enrollment>> GetEnrollments(int studentId)
            => await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .ToListAsync();

        public async Task Add(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
