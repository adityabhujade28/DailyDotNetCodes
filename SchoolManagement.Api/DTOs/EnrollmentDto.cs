namespace SchoolManagement.Api.DTOs;

/// <summary>
/// Enrollment data transfer object
/// </summary>
public class EnrollmentDto
{
    /// <summary>Enrollment Id</summary>
    public int Id { get; set; }

    /// <summary>Student Id for the enrollment</summary>
    public int StudentId { get; set; }

    /// <summary>Student full name</summary>
    public string? StudentName { get; set; }

    /// <summary>Course Id for the enrollment</summary>
    public int CourseId { get; set; }

    /// <summary>Course title</summary>
    public string? CourseName { get; set; }

    /// <summary>Date of enrollment</summary>
    public DateTime EnrollmentDate { get; set; }

    /// <summary>Letter grade</summary>
    public string? Grade { get; set; }

    /// <summary>Numeric grade (0-100)</summary>
    public decimal? NumericGrade { get; set; }
}