using System.ComponentModel.DataAnnotations;


namespace StudentCourseEnrollmentSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credits { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; } = 0; // 0 = unlimited

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Audit & soft-delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
