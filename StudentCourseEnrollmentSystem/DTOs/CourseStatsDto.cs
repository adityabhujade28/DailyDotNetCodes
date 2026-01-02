namespace StudentCourseEnrollmentSystem.DTOs
{
    public class CourseStatsDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int EnrollmentCount { get; set; }
        public decimal? AverageGrade { get; set; }
    }
}