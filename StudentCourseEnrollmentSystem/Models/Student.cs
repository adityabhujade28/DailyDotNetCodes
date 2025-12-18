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
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
