using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
        => await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<List<Enrollment>> GetByStudentIdAsync(int studentId)
        => await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();

    public async Task<List<Enrollment>> GetByCourseIdAsync(int courseId)
        => await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();

    public async Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId)
        => await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

    public async Task<PagedResult<Enrollment>> GetPagedAsync(EnrollmentQueryParameters parameters)
    {
        var query = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).AsQueryable();

        if (parameters.StudentId.HasValue)
            query = query.Where(e => e.StudentId == parameters.StudentId.Value);

        if (parameters.CourseId.HasValue)
            query = query.Where(e => e.CourseId == parameters.CourseId.Value);

        if (parameters.FromDate.HasValue)
            query = query.Where(e => e.EnrollmentDate >= parameters.FromDate.Value);

        if (parameters.ToDate.HasValue)
            query = query.Where(e => e.EnrollmentDate <= parameters.ToDate.Value);

        var total = await query.CountAsync();

        // Sorting
        if (!string.IsNullOrWhiteSpace(parameters.SortBy))
        {
            var sortDir = parameters.SortDir?.ToLower() == "desc" ? "desc" : "asc";
            switch (parameters.SortBy.ToLower())
            {
                case "enrollmentdate":
                    query = sortDir == "desc" ? query.OrderByDescending(e => e.EnrollmentDate) : query.OrderBy(e => e.EnrollmentDate);
                    break;
                case "numericgrade":
                    query = sortDir == "desc" ? query.OrderByDescending(e => e.NumericGrade) : query.OrderBy(e => e.NumericGrade);
                    break;
                default:
                    query = query.OrderBy(e => e.Id);
                    break;
            }
        }
        else
        {
            query = query.OrderBy(e => e.Id);
        }

        var items = await query.Skip((parameters.Page - 1) * parameters.PageSize)
                               .Take(parameters.PageSize)
                               .ToListAsync();

        return new PagedResult<Enrollment>
        {
            Items = items,
            Page = parameters.Page,
            PageSize = parameters.PageSize,
            TotalCount = total
        };
    }

    public async Task AddAsync(Enrollment enrollment)
        => await _context.Enrollments.AddAsync(enrollment);

    public void Update(Enrollment enrollment)
        => _context.Enrollments.Update(enrollment);

    public void Delete(Enrollment enrollment)
        => _context.Enrollments.Remove(enrollment);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}