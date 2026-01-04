using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly Microsoft.Extensions.Logging.ILogger<StudentService> _logger;

    public StudentService(IStudentRepository repository, Microsoft.Extensions.Logging.ILogger<StudentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<StudentResponseDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return students.Adapt<List<StudentResponseDto>>();
    }

    public async Task<PagedResult<StudentResponseDto>> GetPagedAsync(StudentQueryParameters parameters)
    {
        var paged = await _repository.GetPagedAsync(parameters);
        return new PagedResult<StudentResponseDto>
        {
            Items = paged.Items.Adapt<List<StudentResponseDto>>(),
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount
        };
    }

    public async Task<PagedResult<StudentResponseDto>> GetByDepartmentAsync(int departmentId, StudentQueryParameters parameters)
    {
        var paged = await _repository.GetByDepartmentPagedAsync(departmentId, parameters);
        return new PagedResult<StudentResponseDto>
        {
            Items = paged.Items.Adapt<List<StudentResponseDto>>(),
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount
        };
    }

    public async Task<List<StudentPerformanceDto>> GetTopPerformersAsync(int take = 10)
    {
        var results = await _repository.GetTopPerformersAsync(take);
        return results.Select(r => new StudentPerformanceDto
        {
            StudentId = r.student.Id,
            Name = r.student.Name,
            Email = r.student.Email,
            AverageGrade = Math.Round(r.avgGrade, 2)
        }).ToList();
    }

    public async Task<StudentResponseDto?> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        return student?.Adapt<StudentResponseDto>();
    }

    public async Task<StudentDetailDto?> GetByIdWithEnrollmentsAsync(int id)
    {
        var student = await _repository.GetByIdWithEnrollmentsAsync(id);
        return student?.Adapt<StudentDetailDto>();
    }

    public async Task<StudentResponseDto> CreateAsync(StudentCreateDto dto)
    {
        if (dto.DepartmentId.HasValue)
        {
            var exists = await _repository.DepartmentExistsAsync(dto.DepartmentId.Value);
            if (!exists) throw new ArgumentException("Department not found");
        }

        var student = dto.Adapt<Student>();
        
        await _repository.AddAsync(student);
        await _repository.SaveAsync();

        _logger.LogInformation("Student created (Id={Id}, Email={Email})", student.Id, student.Email);

        // Reload student with related Department to include DepartmentName in response
        var saved = await _repository.GetByIdAsync(student.Id);
        return saved!.Adapt<StudentResponseDto>();
    }

    public async Task<StudentResponseDto?> UpdateAsync(int id, StudentUpdateDto dto)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return null;

        if (dto.DepartmentId.HasValue)
        {
            var exists = await _repository.DepartmentExistsAsync(dto.DepartmentId.Value);
            if (!exists) throw new ArgumentException("Department not found");
        }

        dto.Adapt(student); // Map dto onto existing student entity

        _repository.Update(student);
        await _repository.SaveAsync();

        _logger.LogInformation("Student updated (Id={Id})", student.Id);

        // Reload student with related Department to include DepartmentName in response
        var saved = await _repository.GetByIdAsync(student.Id);
        return saved?.Adapt<StudentResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return false;

        _repository.Delete(student);
        await _repository.SaveAsync();

        _logger.LogInformation("Student deleted (Id={Id})", id);
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }
}
