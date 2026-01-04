using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.DTOs;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    [HttpGet("department-statistics")]
    public async Task<IActionResult> DepartmentStatistics()
    {
        var list = await _service.GetDepartmentStatisticsAsync();
        return Ok(ApiResponse<List<DepartmentStatisticsDto>>.SuccessResponse(list));
    }

    [HttpGet("top-courses")]
    public async Task<IActionResult> TopCourses([FromQuery] int take = 10)
    {
        var list = await _service.GetTopCoursesAsync(take);
        return Ok(ApiResponse<List<TopCourseDto>>.SuccessResponse(list));
    }
}