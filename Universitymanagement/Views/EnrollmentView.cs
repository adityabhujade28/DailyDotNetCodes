using UniversityManagement.Services;

namespace UniversityManagement.Views
{
    public class EnrollmentView
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentView(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public async Task EnrollStudent()
        {
            Console.Write("Enter Student Id: ");
            int studentId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Course Id: ");
            int courseId = int.Parse(Console.ReadLine()!);

            try
            {
                await _enrollmentService.EnrollStudent(studentId, courseId);
                Console.WriteLine("Enrollment successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task ViewStudentCourses()
        {
            Console.Write("Enter Student Id: ");
            int studentId = int.Parse(Console.ReadLine()!);

            try
            {
                var courses = await _enrollmentService.GetStudentCourses(studentId);

                Console.WriteLine("\n--- Enrolled Courses ---");
                foreach (var course in courses)
                {
                    Console.WriteLine($"Course Id: {course.Id}, Title: {course.Title}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
