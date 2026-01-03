using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Models;
using SchoolManagement.Api.Interfaces;

namespace SchoolManagement.Api.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudentResponseDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return students.Adapt<List<StudentResponseDto>>();
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
        var student = dto.Adapt<Student>();
        
        await _repository.AddAsync(student);
        await _repository.SaveAsync();

        return student.Adapt<StudentResponseDto>();
    }

    public async Task<StudentResponseDto?> UpdateAsync(int id, StudentUpdateDto dto)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return null;

        dto.Adapt(student); // Map dto onto existing student entity

        _repository.Update(student);
        await _repository.SaveAsync();

        return student.Adapt<StudentResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return false;

        _repository.Delete(student);
        await _repository.SaveAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }
}
