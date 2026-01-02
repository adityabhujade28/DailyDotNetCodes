namespace StudentCourseEnrollmentSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Students.Any() && !context.Courses.Any())
            {
                var students = new[] {
                    new Models.Student { StudentName = "Alice Wonderland", Email = "alice@example.com" },
                    new Models.Student { StudentName = "Bob Builder", Email = "bob@example.com" },
                    new Models.Student { StudentName = "Carol Singer", Email = "carol@example.com" }
                };

                var courses = new[] {
                    new Models.Course { CourseName = "Introduction to C#", Credits = 3 },
                    new Models.Course { CourseName = "Database Systems", Credits = 4 },
                    new Models.Course { CourseName = "Algorithms", Credits = 3 }
                };

                context.Students.AddRange(students);
                context.Courses.AddRange(courses);
                context.SaveChanges();

                // Enroll Alice and Bob
                context.Enrollments.Add(new Models.Enrollment { StudentId = students[0].StudentId, CourseId = courses[0].CourseId, Grade = 3.7m, Status = Models.EnrollmentStatus.Active });
                context.Enrollments.Add(new Models.Enrollment { StudentId = students[1].StudentId, CourseId = courses[1].CourseId, Grade = 3.2m, Status = Models.EnrollmentStatus.Active });
                context.Enrollments.Add(new Models.Enrollment { StudentId = students[2].StudentId, CourseId = courses[2].CourseId, Grade = 3.9m, Status = Models.EnrollmentStatus.Active });
                context.SaveChanges();
            }
        }
    }
}
