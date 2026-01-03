using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync()
        => await _context.Students.ToListAsync();

    public async Task<Student?> GetByIdAsync(int id)
        => await _context.Students.FindAsync(id);

    public async Task<Student?> GetByIdWithEnrollmentsAsync(int id)
        => await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Student?> GetByEmailAsync(string email)
        => await _context.Students.FirstOrDefaultAsync(s => s.Email == email);

    public async Task AddAsync(Student student)
        => await _context.Students.AddAsync(student);

    public void Update(Student student)
        => _context.Students.Update(student);

    public void Delete(Student student)
        => _context.Students.Remove(student);

    public async Task<bool> ExistsAsync(int id)
        => await _context.Students.AnyAsync(s => s.Id == id);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}
