using System.Collections.Generic;

namespace UniversityManagement.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();
    }
}
