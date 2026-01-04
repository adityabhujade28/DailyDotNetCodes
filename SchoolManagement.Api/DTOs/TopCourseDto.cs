namespace SchoolManagement.Api.DTOs;

public class TopCourseDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public int EnrollmentCount { get; set; }
    public decimal? AverageGrade { get; set; }
}