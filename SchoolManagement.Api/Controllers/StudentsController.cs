using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all students
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<StudentResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([
        FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? name = null,
        [FromQuery] string? email = null,
        [FromQuery] int? courseId = null,
        [FromQuery] int? departmentId = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortDir = null)
    {
        var parameters = new StudentQueryParameters
        {
            Page = page,
            PageSize = pageSize,
            Name = name,
            Email = email,
            CourseId = courseId,
            DepartmentId = departmentId,
            Search = search,
            SortBy = sortBy,
            SortDir = sortDir
        };

        var result = await _service.GetPagedAsync(parameters);
        var meta = new { result.Page, result.PageSize, result.TotalCount, result.TotalPages };
        return Ok(ApiResponse<PagedResult<StudentResponseDto>>.SuccessResponse(result, 200, meta));
    }

    /// <summary>
    /// Get a student by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _service.GetByIdAsync(id);
        return student == null ? NotFound(ApiResponse.Failure("Student not found", 404)) : Ok(ApiResponse<StudentResponseDto>.SuccessResponse(student));
    }

    /// <summary>
    /// Get a student by ID with enrollments
    /// </summary>
    [HttpGet("{id}/details")]
    [ProducesResponseType(typeof(StudentDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithEnrollments(int id)
    {
        var student = await _service.GetByIdWithEnrollmentsAsync(id);
        return student == null ? NotFound(ApiResponse.Failure("Student not found", 404)) : Ok(ApiResponse<StudentDetailDto>.SuccessResponse(student));
    }

    /// <summary>
    /// Get students by department (paginated)
    /// </summary>
    [HttpGet("department/{departmentId}")]
    [ProducesResponseType(typeof(PagedResult<StudentResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDepartment(int departmentId, [FromQuery] StudentQueryParameters parameters)
    {
        var paged = await _service.GetByDepartmentAsync(departmentId, parameters);
        var meta = new { paged.Page, paged.PageSize, paged.TotalCount, paged.TotalPages };
        return Ok(ApiResponse<PagedResult<StudentResponseDto>>.SuccessResponse(paged, 200, meta));
    }

    /// <summary>
    /// Get top performers by average numeric grade
    /// </summary>
    [HttpGet("top-performers")]
    [ProducesResponseType(typeof(List<StudentPerformanceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopPerformers([FromQuery] int take = 10)
    {
        var list = await _service.GetTopPerformersAsync(take);
        return Ok(ApiResponse<List<StudentPerformanceDto>>.SuccessResponse(list));
    }

    /// <summary>
    /// Create a new student
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var student = await _service.CreateAsync(dto);
            var payload = ApiResponse<StudentResponseDto>.SuccessResponse(student, 201);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, payload);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message, 400));
        }
    }

    /// <summary>
    /// Update an existing student
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var student = await _service.UpdateAsync(id, dto);
            return student == null ? NotFound(ApiResponse.Failure("Student not found", 404)) : Ok(ApiResponse<StudentResponseDto>.SuccessResponse(student));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message, 400));
        }
    }

    /// <summary>
    /// Delete a student
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (deleted == null) return NotFound(ApiResponse.Failure("Student not found", 404));

        var response = ApiResponse<StudentResponseDto>.SuccessResponse(deleted, 200);
        response.Message = $"{deleted.Name} has been deleted.";
        return Ok(response);
    }
}
