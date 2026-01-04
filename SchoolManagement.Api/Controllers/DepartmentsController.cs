using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentsController(IDepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(ApiResponse<List<DepartmentDto>>.SuccessResponse(list));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var dep = await _service.GetByIdAsync(id);
        return dep == null ? NotFound(ApiResponse.Failure("Department not found", 404)) : Ok(ApiResponse<DepartmentDto>.SuccessResponse(dep));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse.Failure("Invalid payload", 400));

        var dep = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = dep.Id }, ApiResponse<DepartmentDto>.SuccessResponse(dep, 201));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DepartmentUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse.Failure("Invalid payload", 400));

        var dep = await _service.UpdateAsync(id, dto);
        return dep == null ? NotFound(ApiResponse.Failure("Department not found", 404)) : Ok(ApiResponse<DepartmentDto>.SuccessResponse(dep));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound(ApiResponse.Failure("Department not found", 404));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message, 400));
        }
    }
}