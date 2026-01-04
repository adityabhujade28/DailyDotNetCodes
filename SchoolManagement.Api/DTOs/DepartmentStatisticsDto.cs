namespace SchoolManagement.Api.DTOs;

public class DepartmentStatisticsDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int StudentCount { get; set; }
    public int TotalEnrollments { get; set; }
    public decimal? AverageGrade { get; set; }
}