using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repository;
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly Microsoft.Extensions.Logging.ILogger<EnrollmentService> _logger;

    public EnrollmentService(IEnrollmentRepository repository, IStudentRepository studentRepo, ICourseRepository courseRepo, Microsoft.Extensions.Logging.ILogger<EnrollmentService> logger)
    {
        _repository = repository;
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
        _logger = logger;
    }

    public async Task<EnrollmentDto> CreateAsync(EnrollmentCreateDto dto)
    {
        // Validate student and course
        var student = await _studentRepo.GetByIdAsync(dto.StudentId);
        if (student == null) throw new ArgumentException("Student not found");

        var course = await _courseRepo.GetByIdAsync(dto.CourseId);
        if (course == null) throw new ArgumentException("Course not found");

        // Prevent duplicate enrollment
        var exists = await _repository.GetByStudentAndCourseAsync(dto.StudentId, dto.CourseId);
        if (exists != null) throw new ArgumentException("Student is already enrolled in this course");

        // Prevent cross-department enrollment when both sides have departments assigned
        if (student.DepartmentId.HasValue && course.DepartmentId.HasValue
            && student.DepartmentId.Value != course.DepartmentId.Value)
        {
            throw new ArgumentException("Student cannot enroll in a course from a different department");
        }

        // Validate numeric grade if present
        if (dto.NumericGrade.HasValue && (dto.NumericGrade < 0 || dto.NumericGrade > 100))
            throw new ArgumentException("NumericGrade must be between 0 and 100");

        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            EnrollmentDate = dto.EnrollmentDate ?? DateTime.UtcNow,
            Grade = dto.Grade,
            NumericGrade = dto.NumericGrade
        };

        await _repository.AddAsync(enrollment);
        await _repository.SaveAsync();

        _logger.LogInformation("Enrollment created (Id={Id}, StudentId={StudentId}, CourseId={CourseId})", enrollment.Id, enrollment.StudentId, enrollment.CourseId);

        // Reload to include navigation properties
        var created = await _repository.GetByIdAsync(enrollment.Id);
        return created!.Adapt<EnrollmentDto>();
    }

    public async Task<PagedResult<EnrollmentDto>> GetPagedAsync(EnrollmentQueryParameters parameters)
    {
        var paged = await _repository.GetPagedAsync(parameters);
        return new PagedResult<EnrollmentDto>
        {
            Items = paged.Items.Adapt<List<EnrollmentDto>>(),
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount
        };
    }

    public async Task<List<EnrollmentDto>> GetByStudentAsync(int studentId)
    {
        var list = await _repository.GetByStudentIdAsync(studentId);
        return list.Adapt<List<EnrollmentDto>>();
    }

    public async Task<List<EnrollmentDto>> GetByCourseAsync(int courseId)
    {
        var list = await _repository.GetByCourseIdAsync(courseId);
        return list.Adapt<List<EnrollmentDto>>();
    }

    public async Task<EnrollmentDto?> UpdateAsync(int id, EnrollmentUpdateDto dto)
    {
        var enrollment = await _repository.GetByIdAsync(id);
        if (enrollment == null) return null;

        if (dto.NumericGrade.HasValue && (dto.NumericGrade < 0 || dto.NumericGrade > 100))
            throw new ArgumentException("NumericGrade must be between 0 and 100");

        dto.Adapt(enrollment);
        _repository.Update(enrollment);
        await _repository.SaveAsync();

        _logger.LogInformation("Enrollment updated (Id={Id})", id);

        var updated = await _repository.GetByIdAsync(id);
        return updated?.Adapt<EnrollmentDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var enrollment = await _repository.GetByIdAsync(id);
        if (enrollment == null) return false;
        _repository.Delete(enrollment);
        await _repository.SaveAsync();

        _logger.LogInformation("Enrollment deleted (Id={Id})", id);
        return true;
    }
}