using System;
using System.Collections.Generic;


namespace Pagination.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Grade { get; set; }

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }

}
