using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Api.DTOs;

public class DepartmentCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}