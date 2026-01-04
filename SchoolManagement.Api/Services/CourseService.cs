using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly Microsoft.Extensions.Logging.ILogger<CourseService> _logger;

    public CourseService(ICourseRepository repository, IDepartmentRepository departmentRepository, Microsoft.Extensions.Logging.ILogger<CourseService> logger)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
        _logger = logger;
    }

    public async Task<List<CourseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Adapt<List<CourseDto>>();
    }

    public async Task<PagedResult<CourseDto>> GetPagedAsync(CourseQueryParameters parameters)
    {
        var paged = await _repository.GetPagedAsync(parameters);
        return new PagedResult<CourseDto>
        {
            Items = paged.Items.Adapt<List<CourseDto>>(),
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount
        };
    }

    public async Task<List<CourseDto>> GetByDepartmentAsync(int departmentId)
    {
        var list = await _repository.GetByDepartmentAsync(departmentId);
        return list.Adapt<List<CourseDto>>();
    }

    public async Task<CourseDto?> GetByIdAsync(int id)
    {
        var course = await _repository.GetByIdAsync(id);
        return course?.Adapt<CourseDto>();
    }

    public async Task<CourseDto> CreateAsync(CourseCreateDto dto)
    {
        // Department validation
        if (!dto.DepartmentId.HasValue) throw new ArgumentException("DepartmentId is required");
        var deptExists = await _departmentRepository.ExistsAsync(dto.DepartmentId.Value);
        if (!deptExists) throw new ArgumentException("Department not found");

        // Unique title validation
        var existing = await _repository.GetByTitleAsync(dto.Title);
        if (existing != null)
            throw new ArgumentException("Course title already exists");

        var course = dto.Adapt<Course>();
        await _repository.AddAsync(course);
        await _repository.SaveAsync();

        _logger.LogInformation("Course created (Id={Id}, Title={Title})", course.Id, course.Title);
        // Reload to include related Department
        var saved = await _repository.GetByIdAsync(course.Id);
        return saved!.Adapt<CourseDto>();
    }

    public async Task<CourseDto?> UpdateAsync(int id, CourseUpdateDto dto)
    {
        var course = await _repository.GetByIdAsync(id);
        if (course == null) return null;

        // If title changed, ensure uniqueness
        if (!string.Equals(course.Title, dto.Title, StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _repository.GetByTitleAsync(dto.Title);
            if (exists != null) throw new ArgumentException("Course title already exists");
        }

        dto.Adapt(course);
        _repository.Update(course);
        await _repository.SaveAsync();

        _logger.LogInformation("Course updated (Id={Id})", course.Id);
        // Reload to include related Department
        var saved = await _repository.GetByIdAsync(course.Id);
        return saved?.Adapt<CourseDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await _repository.GetByIdAsync(id);
        if (course == null) return false;
        _repository.Delete(course);
        await _repository.SaveAsync();

        _logger.LogInformation("Course deleted (Id={Id})", id);
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }
}