using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Api.DTOs;

public class EnrollmentCreateDto
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string? Grade { get; set; }

    public decimal? NumericGrade { get; set; }
}