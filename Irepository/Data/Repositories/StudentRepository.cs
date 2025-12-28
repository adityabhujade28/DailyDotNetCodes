using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Interfaces;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Data.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Student>> GetActiveStudentsAsync()
        => await _context.Students.Where(s => s.IsActive).ToListAsync();
}
