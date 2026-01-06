using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _service;
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;

    public EnrollmentsController(IEnrollmentService service, IStudentService studentService, ICourseService courseService)
    {
        _service = service;
        _studentService = studentService;
        _courseService = courseService;
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

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<EnrollmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([
        FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? studentId = null,
        [FromQuery] int? courseId = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortDir = null)
    {
        var parameters = new EnrollmentQueryParameters
        {
            Page = page,
            PageSize = pageSize,
            StudentId = studentId,
            CourseId = courseId,
            FromDate = fromDate,
            ToDate = toDate,
            SortBy = sortBy,
            SortDir = sortDir
        };

        // If caller filtered by student or course, ensure the parent exists so we can return a clear 404
        if (studentId.HasValue)
        {
            var exists = await _studentService.ExistsAsync(studentId.Value);
            if (!exists) return NotFound(ApiResponse.Failure("Student not found", 404));
        }

        if (courseId.HasValue)
        {
            var exists = await _courseService.ExistsAsync(courseId.Value);
            if (!exists) return NotFound(ApiResponse.Failure("Course not found", 404));
        }

        var result = await _service.GetPagedAsync(parameters);
        object meta = new { result.Page, result.PageSize, result.TotalCount, result.TotalPages };

        // If there are no results but the parent exists, include a friendly message in metadata
        if (result.TotalCount == 0)
        {
            if (studentId.HasValue)
            {
                meta = new { result.Page, result.PageSize, result.TotalCount, result.TotalPages, Message = $"No enrollments found for student {studentId.Value}" };
            }
            else if (courseId.HasValue)
            {
                meta = new { result.Page, result.PageSize, result.TotalCount, result.TotalPages, Message = $"No enrollments found for course {courseId.Value}" };
            }
        }

        return Ok(ApiResponse<PagedResult<EnrollmentDto>>.SuccessResponse(result, 200, meta));
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetByStudent(int studentId)
    {
        // Verify the student exists to provide a clear 404 when appropriate
        var exists = await _studentService.ExistsAsync(studentId);
        if (!exists)
            return NotFound(ApiResponse.Failure("Student not found", 404));

        var list = await _service.GetByStudentAsync(studentId) ?? new List<EnrollmentDto>();

        if (!list.Any())
        {
            var meta = new { Message = $"No enrollments found for student {studentId}", Count = 0 };
            return Ok(ApiResponse<List<EnrollmentDto>>.SuccessResponse(list, 200, meta));
        }

        return Ok(ApiResponse<List<EnrollmentDto>>.SuccessResponse(list));
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        // Verify the course exists to provide a clear 404 when appropriate
        var exists = await _courseService.ExistsAsync(courseId);
        if (!exists)
            return NotFound(ApiResponse.Failure("Course not found", 404));

        var list = await _service.GetByCourseAsync(courseId) ?? new List<EnrollmentDto>();

        if (!list.Any())
        {
            var meta = new { Message = $"No enrollments found for course {courseId}", Count = 0 };
            return Ok(ApiResponse<List<EnrollmentDto>>.SuccessResponse(list, 200, meta));
        }

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