using System;
using System.Collections.Generic;

namespace Pagination.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = null!;
        public double GPA { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
