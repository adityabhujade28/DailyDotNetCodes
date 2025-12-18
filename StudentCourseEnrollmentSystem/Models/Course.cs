using System.ComponentModel.DataAnnotations;


namespace StudentCourseEnrollmentSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [Range(1, 10)]
        public int Credits { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
