using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;

    public CoursesController(ICourseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(ApiResponse<List<CourseDto>>.SuccessResponse(list));
    }

    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetByDepartment(int departmentId)
    {
        var list = await _service.GetByDepartmentAsync(departmentId);
        return Ok(ApiResponse<List<CourseDto>>.SuccessResponse(list));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var course = await _service.GetByIdAsync(id);
        return course == null ? NotFound(ApiResponse.Failure("Course not found", 404)) : Ok(ApiResponse<CourseDto>.SuccessResponse(course));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse.Failure("Invalid payload", 400));

        try
        {
            var course = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = course.Id }, ApiResponse<CourseDto>.SuccessResponse(course, 201));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message, 400));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CourseUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse.Failure("Invalid payload", 400));

        try
        {
            var course = await _service.UpdateAsync(id, dto);
            return course == null ? NotFound(ApiResponse.Failure("Course not found", 404)) : Ok(ApiResponse<CourseDto>.SuccessResponse(course));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message, 400));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound(ApiResponse.Failure("Course not found", 404));
    }
}