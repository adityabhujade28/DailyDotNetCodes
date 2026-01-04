using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Api.DTOs;

public class EnrollmentUpdateDto
{
    [StringLength(5)]
    public string? Grade { get; set; }

    public decimal? NumericGrade { get; set; }
}