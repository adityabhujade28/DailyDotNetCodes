using System;
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
        public Student? Student { get; set; }
        public Course? Course { get; set; }

        // Payload data
        public DateTime EnrolledOn { get; set; } = DateTime.Now;

        [Range(0, 4)]
        public decimal? Grade { get; set; }

        [Required]
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;

        // Audit & soft-delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
    }
} 
