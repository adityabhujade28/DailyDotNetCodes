using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Utilities;

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

    public async Task<PagedResult<Course>> GetPagedAsync(CourseQueryParameters parameters)
    {
        var query = _context.Courses.Include(c => c.Department).AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Title))
            query = query.Where(c => c.Title.Contains(parameters.Title));

        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(c => c.Title.Contains(parameters.Search) || (c.Description != null && c.Description.Contains(parameters.Search)));

        if (parameters.DepartmentId.HasValue)
            query = query.Where(c => c.DepartmentId == parameters.DepartmentId.Value);

        var total = await query.CountAsync();

        // Sorting (use helpers to reduce duplication)
        if (!string.IsNullOrWhiteSpace(parameters.SortBy))
        {
            switch (parameters.SortBy.ToLower())
            {
                case "title":
                    query = query.OrderByDirection(c => c.Title, parameters.SortDir);
                    break;
                case "credits":
                    query = query.OrderByDirection(c => c.Credits, parameters.SortDir);
                    break;
                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }
        }
        else
        {
            query = query.OrderBy(c => c.Id);
        }

        // Paging (shared helper)
        var items = await query.ApplyPaging(parameters.Page, parameters.PageSize).ToListAsync();

        return new PagedResult<Course>
        {
            Items = items,
            Page = parameters.Page,
            PageSize = parameters.PageSize,
            TotalCount = total
        };
    }

    public async Task<List<Course>> GetByDepartmentAsync(int departmentId)
        => await _context.Courses.Where(c => c.DepartmentId == departmentId).Include(c => c.Department).ToListAsync();

    public async Task<Course?> GetByIdAsync(int id)
        => await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Course?> GetByTitleAsync(string title)
        => await _context.Courses.FirstOrDefaultAsync(c => c.Title == title);

    public async Task AddAsync(Course course)
        => await _context.Courses.AddAsync(course);

    public void Update(Course course)
        => _context.Courses.Update(course);

    public void Delete(Course course)
        => _context.Courses.Remove(course);

    public async Task<bool> HasEnrollmentsAsync(int courseId)
        => await _context.Enrollments.AnyAsync(e => e.CourseId == courseId);

    public async Task<bool> ExistsAsync(int id)
        => await _context.Courses.AnyAsync(c => c.Id == id);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}