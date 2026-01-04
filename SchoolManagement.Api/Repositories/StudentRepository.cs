using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.DTOs;

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

    public async Task<PagedResult<Student>> GetPagedAsync(StudentQueryParameters parameters)
    {
        // Include Department so DTOs can show department name
        // Force a single-query include so Department navigation is materialized in the result
        var query = _context.Students
            .Include(s => s.Department)
            .AsQueryable()
            .AsSingleQuery();

        // Simple filters
        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(s => s.Name.Contains(parameters.Name));

        if (!string.IsNullOrWhiteSpace(parameters.Email))
            query = query.Where(s => s.Email.Contains(parameters.Email));

        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(s => s.Name.Contains(parameters.Search) || s.Email.Contains(parameters.Search));

        if (parameters.CourseId.HasValue)
            query = query.Where(s => s.Enrollments.Any(e => e.CourseId == parameters.CourseId.Value));

        if (parameters.DepartmentId.HasValue)
            query = query.Where(s => s.DepartmentId == parameters.DepartmentId.Value);

        var total = await query.CountAsync();

        // Sorting
        if (!string.IsNullOrWhiteSpace(parameters.SortBy))
        {
            var sortDir = parameters.SortDir?.ToLower() == "desc" ? "desc" : "asc";
            switch (parameters.SortBy.ToLower())
            {
                case "name":
                    query = sortDir == "desc" ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name);
                    break;
                case "email":
                    query = sortDir == "desc" ? query.OrderByDescending(s => s.Email) : query.OrderBy(s => s.Email);
                    break;
                case "createdat":
                    query = sortDir == "desc" ? query.OrderByDescending(s => s.CreatedAt) : query.OrderBy(s => s.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(s => s.Id);
                    break;
            }
        }
        else
        {
            query = query.OrderBy(s => s.Id);
        }

        var items = await query.Skip((parameters.Page - 1) * parameters.PageSize)
                               .Take(parameters.PageSize)
                               .ToListAsync();

        return new PagedResult<Student>
        {
            Items = items,
            Page = parameters.Page,
            PageSize = parameters.PageSize,
            TotalCount = total
        };
    }

    public async Task<PagedResult<Student>> GetByDepartmentPagedAsync(int departmentId, StudentQueryParameters parameters)
    {
        parameters.DepartmentId = departmentId;
        return await GetPagedAsync(parameters);
    }

    public async Task<List<(Student student, decimal avgGrade)>> GetTopPerformersAsync(int take = 10)
    {
        // Average numeric grade per student where NumericGrade is not null
        var query = _context.Enrollments
            .Where(e => e.NumericGrade.HasValue)
            .GroupBy(e => e.StudentId)
            .Select(g => new { StudentId = g.Key, Avg = g.Average(e => e.NumericGrade!.Value) })
            .OrderByDescending(x => x.Avg)
            .Take(take);

        var results = await query.ToListAsync();

        var students = await _context.Students
            .Where(s => results.Select(r => r.StudentId).Contains(s.Id))
            .ToListAsync();

        // join results with students preserving avg
        var joined = results.Join(students, r => r.StudentId, s => s.Id, (r, s) => (student: s, avgGrade: r.Avg)).ToList();

        return joined;
    }

    public async Task<bool> DepartmentExistsAsync(int departmentId)
    {
        return await _context.Departments.AnyAsync(d => d.Id == departmentId);
    }

    public async Task<Student?> GetByIdAsync(int id)
        => await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Student?> GetByIdWithEnrollmentsAsync(int id)
        => await _context.Students
            .Include(s => s.Department)
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
