namespace StudentCourseEnrollmentSystem.DTOs
{
    public class EnrollmentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        public decimal? Grade { get; set; }
        public DateTime EnrolledOn { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
