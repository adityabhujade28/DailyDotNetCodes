using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllAsync()
        => await _context.Courses.Include(c => c.Department).ToListAsync();

    public async Task<List<Course>> GetByDepartmentAsync(int departmentId)
        => await _context.Courses.Where(c => c.DepartmentId == departmentId).Include(c => c.Department).ToListAsync();

    public async Task<Course?> GetByIdAsync(int id)
        => await _context.Courses.FindAsync(id);

    public async Task<Course?> GetByTitleAsync(string title)
        => await _context.Courses.FirstOrDefaultAsync(c => c.Title == title);

    public async Task AddAsync(Course course)
        => await _context.Courses.AddAsync(course);

    public void Update(Course course)
        => _context.Courses.Update(course);

    public void Delete(Course course)
        => _context.Courses.Remove(course);

    public async Task<bool> ExistsAsync(int id)
        => await _context.Courses.AnyAsync(c => c.Id == id);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}