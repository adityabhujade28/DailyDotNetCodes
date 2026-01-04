using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Interfaces;

public interface IReportService
{
    Task<List<DepartmentStatisticsDto>> GetDepartmentStatisticsAsync();
    Task<List<TopCourseDto>> GetTopCoursesAsync(int take = 10);
}