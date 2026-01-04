using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _service;

    public EnrollmentsController(IEnrollmentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EnrollmentCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse.Failure("Invalid payload", 400));

        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByStudent), new { studentId = created.StudentId }, ApiResponse<EnrollmentDto>.SuccessResponse(created, 201));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message, 400));
        }
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetByStudent(int studentId)
    {
        var list = await _service.GetByStudentAsync(studentId);
        return Ok(ApiResponse<List<EnrollmentDto>>.SuccessResponse(list));
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        var list = await _service.GetByCourseAsync(courseId);
        return Ok(ApiResponse<List<EnrollmentDto>>.SuccessResponse(list));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EnrollmentUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse.Failure("Invalid payload", 400));

        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound(ApiResponse.Failure("Enrollment not found", 404)) : Ok(ApiResponse<EnrollmentDto>.SuccessResponse(updated));
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
        return ok ? NoContent() : NotFound(ApiResponse.Failure("Enrollment not found", 404));
    }
}