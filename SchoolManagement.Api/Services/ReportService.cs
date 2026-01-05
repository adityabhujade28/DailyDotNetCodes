using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Data;

namespace SchoolManagement.Api.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentStatisticsDto>> GetDepartmentStatisticsAsync()
    {
        // For each department: student count, total enrollments, average numeric grade across enrollments
        var query = from d in _context.Departments
                    join s in _context.Students on d.Id equals s.DepartmentId into sd
                    select new DepartmentStatisticsDto
                    {
                        DepartmentId = d.Id,
                        DepartmentName = d.Name,
                        StudentCount = sd.Count(),
                        TotalEnrollments = sd.SelectMany(st => st.Enrollments).Count(),
                        AverageGrade = sd.SelectMany(st => st.Enrollments)
                                         .Where(e => e.NumericGrade.HasValue)
                                         .Average(e => (decimal?)e.NumericGrade)
                    };

        return await query.ToListAsync();
    }

    public async Task<List<TopCourseDto>> GetTopCoursesAsync(int take = 10)
    {
        // Top courses by enrollment count and average numeric grade (if present)
        var results = await _context.Enrollments
            .GroupBy(e => e.CourseId)
            .Select(g => new
            {
                CourseId = g.Key,
                Count = g.Count(),
                Avg = g.Where(e => e.NumericGrade.HasValue).Average(e => (decimal?)e.NumericGrade)
            })
            .OrderByDescending(x => x.Count)
            .Take(take)
            .ToListAsync();

        var courseIds = results.Select(r => r.CourseId).ToList();
        var courses = await _context.Courses.Where(c => courseIds.Contains(c.Id)).ToListAsync();

        var list = results.Join(courses, r => r.CourseId, c => c.Id, (r, c) => new TopCourseDto
        {
            CourseId = c.Id,
            CourseTitle = c.Title,
            EnrollmentCount = r.Count,
            AverageGrade = r.Avg.HasValue ? (decimal?)Math.Round(r.Avg.Value, 2) : null
        }).ToList();

        return list;
    }

    private static string SemesterLabel(DateTime dt)
    {
        var year = dt.Year;
        var half = dt.Month <= 6 ? "S1" : "S2";
        return $"{year}-{half}";
    }
}