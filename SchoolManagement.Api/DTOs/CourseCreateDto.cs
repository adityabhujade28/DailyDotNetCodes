using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Api.DTOs;

/// <summary>
/// Payload to create a course
/// </summary>
public class CourseCreateDto
{
    /// <summary>Course title</summary>
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    /// <summary>Optional description</summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>Course credits</summary>
    public int Credits { get; set; } = 0;

    /// <summary>Department id this course belongs to</summary>
    [Required(ErrorMessage = "DepartmentId is required")]
    public int? DepartmentId { get; set; }
}