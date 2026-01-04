#pragma warning disable CS8603 // Possible null reference return
using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Mappings;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        // Student -> StudentResponseDto
        TypeAdapterConfig<Student, StudentResponseDto>.NewConfig()
            .Map(dest => dest.DepartmentName, src => src.Department != null ? src.Department.Name : string.Empty)
            .Map(dest => dest.DepartmentId, src => src.DepartmentId);

        // Student -> StudentDetailDto (with enrollments)
        TypeAdapterConfig<Student, StudentDetailDto>.NewConfig()
            .Map(dest => dest.Enrollments, src => src.Enrollments ?? new List<Enrollment>())
            .Map(dest => dest.DepartmentName, src => src.Department != null ? src.Department.Name : string.Empty)
            .Map(dest => dest.DepartmentId, src => src.DepartmentId);

        // Department mappings
        TypeAdapterConfig<Department, DepartmentDto>.NewConfig();
        TypeAdapterConfig<DepartmentCreateDto, Department>.NewConfig();
        TypeAdapterConfig<DepartmentUpdateDto, Department>.NewConfig();

        // Enrollment mappings
        TypeAdapterConfig<Enrollment, EnrollmentDto>.NewConfig()
            .Map(dest => dest.StudentName, src => src.Student != null ? src.Student.Name : string.Empty)
            .Map(dest => dest.CourseName, src => src.Course != null ? src.Course.Title : string.Empty)
            .Map(dest => dest.NumericGrade, src => src.NumericGrade);

        TypeAdapterConfig<EnrollmentCreateDto, Enrollment>.NewConfig();
        TypeAdapterConfig<EnrollmentUpdateDto, Enrollment>.NewConfig();

        // Course mappings
        TypeAdapterConfig<Course, CourseDto>.NewConfig()
            .Map(dest => dest.DepartmentId, src => src.DepartmentId)
            .Map(dest => dest.DepartmentName, src => src.Department != null ? src.Department.Name : string.Empty);
        TypeAdapterConfig<CourseCreateDto, Course>.NewConfig();
        TypeAdapterConfig<CourseUpdateDto, Course>.NewConfig();

        // StudentCreateDto -> Student
        TypeAdapterConfig<StudentCreateDto, Student>.NewConfig()
            .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.UpdatedAt)
            .Ignore(dest => dest.Enrollments);

        // StudentUpdateDto -> Student (for partial update)
        TypeAdapterConfig<StudentUpdateDto, Student>.NewConfig()
            .Map(dest => dest.UpdatedAt, _ => DateTime.UtcNow)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.Enrollments);
    }
}
#pragma warning restore CS8603 // Possible null reference return
