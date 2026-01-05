using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.Exceptions;

namespace SchoolManagement.Api.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly Microsoft.Extensions.Logging.ILogger<DepartmentService> _logger;

    public DepartmentService(IDepartmentRepository repository, Microsoft.Extensions.Logging.ILogger<DepartmentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var deps = await _repository.GetAllAsync();
        return deps.Adapt<List<DepartmentDto>>();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var dep = await _repository.GetByIdAsync(id);
        return dep?.Adapt<DepartmentDto>();
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto)
    {
        // Prevent duplicate department names at service boundary for clearer errors
        var existing = await _repository.GetByNameAsync(dto.Name);
        if (existing != null)
            throw new ConflictException("Department with the same name already exists.");

        var dep = dto.Adapt<Department>();
        await _repository.AddAsync(dep);
        await _repository.SaveAsync();

        _logger.LogInformation("Department created (Id={Id}, Name={Name})", dep.Id, dep.Name);
        return dep.Adapt<DepartmentDto>();
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, DepartmentUpdateDto dto)
    {
        var dep = await _repository.GetByIdAsync(id);
        if (dep == null) return null;
        dto.Adapt(dep);
        _repository.Update(dep);
        await _repository.SaveAsync();

        _logger.LogInformation("Department updated (Id={Id})", dep.Id);
        return dep.Adapt<DepartmentDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dep = await _repository.GetByIdAsync(id);
        if (dep == null) return false;

        if (await _repository.HasStudentsAsync(id))
            throw new ArgumentException("Department has assigned students and cannot be deleted");

        if (await _repository.HasCoursesAsync(id))
            throw new ArgumentException("Department has assigned courses and cannot be deleted");

        _repository.Delete(dep);
        await _repository.SaveAsync();

        _logger.LogInformation("Department deleted (Id={Id})", id);
        return true;
    }
}