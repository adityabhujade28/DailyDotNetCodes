namespace StudentCourseEnrollmentSystem.DTOs
{
    public class StudentPerformanceDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public decimal AverageGrade { get; set; }
        public int CourseCount { get; set; }
    }
}