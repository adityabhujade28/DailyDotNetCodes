using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCourseEnrollmentSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Audit & soft-delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
