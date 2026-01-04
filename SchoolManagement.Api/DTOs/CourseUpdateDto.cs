using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Api.DTOs;

public class CourseUpdateDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public int Credits { get; set; } = 0;

    [Required(ErrorMessage = "DepartmentId is required")]
    public int? DepartmentId { get; set; }
}