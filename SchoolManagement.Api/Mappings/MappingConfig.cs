using Mapster;
using SchoolManagement.Api.DTOs;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Mappings;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        // Student -> StudentResponseDto
        TypeAdapterConfig<Student, StudentResponseDto>.NewConfig();

        // Student -> StudentDetailDto (with enrollments)
        TypeAdapterConfig<Student, StudentDetailDto>.NewConfig()
            .Map(dest => dest.Enrollments, src => src.Enrollments);

        // Enrollment -> EnrollmentDto (map nested Course.Title to CourseName)
        TypeAdapterConfig<Enrollment, EnrollmentDto>.NewConfig()
            .Map(dest => dest.CourseName, src => src.Course.Title);

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
