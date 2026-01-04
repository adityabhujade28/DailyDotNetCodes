namespace SchoolManagement.Api.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Credits { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
}