namespace SchoolManagement.Api.DTOs;

/// <summary>
/// Course data transfer object returned by the API.
/// </summary>
public class CourseDto
{
    /// <summary>Course primary key</summary>
    public int Id { get; set; }

    /// <summary>Course title</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Optional course description</summary>
    public string? Description { get; set; }

    /// <summary>Number of credits</summary>
    public int Credits { get; set; }

    /// <summary>Identifier of the department the course belongs to</summary>
    public int? DepartmentId { get; set; }

    /// <summary>Department name (populated when available)</summary>
    public string? DepartmentName { get; set; }
}