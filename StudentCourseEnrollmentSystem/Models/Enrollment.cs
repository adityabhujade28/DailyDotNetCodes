using System.ComponentModel.DataAnnotations;

namespace StudentCourseEnrollmentSystem.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        // Foreign Keys
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        // Navigation Properties
        public Student Student { get; set; }
        public Course Course { get; set; }

        // Payload data
        public DateTime EnrolledOn { get; set; } = DateTime.Now;

        [Range(0, 4)]
        public decimal? Grade { get; set; }

        [Required]
        public string Status { get; set; } = "Active";
    }
}
