using System.ComponentModel.DataAnnotations;

namespace StudentCourseEnrollmentSystem.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }

        public DateTime EnrolledOn { get; set; } = DateTime.Now;

        [Range(0, 4)]
        public decimal? Grade { get; set; }

        [Required]
        public string Status { get; set; } = "Active";
    }
}
