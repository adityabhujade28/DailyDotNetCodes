using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetAllAsync()
        => await _context.Departments.ToListAsync();

    public async Task<Department?> GetByIdAsync(int id)
        => await _context.Departments.FindAsync(id);

    public async Task<Department?> GetByNameAsync(string name)
        => await _context.Departments.FirstOrDefaultAsync(d => d.Name == name);

    public async Task AddAsync(Department department)
        => await _context.Departments.AddAsync(department);

    public void Update(Department department)
        => _context.Departments.Update(department);

    public void Delete(Department department)
        => _context.Departments.Remove(department);

    public async Task<bool> ExistsAsync(int id)
        => await _context.Departments.AnyAsync(d => d.Id == id);

    public async Task<bool> HasStudentsAsync(int departmentId)
        => await _context.Students.AnyAsync(s => s.DepartmentId == departmentId);

    public async Task<bool> HasCoursesAsync(int departmentId)
        => await _context.Courses.AnyAsync(c => c.DepartmentId == departmentId);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}