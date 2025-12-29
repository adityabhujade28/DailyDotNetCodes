using UniversityManagement.Interfaces;

namespace UniversityManagement.Views
{
    public class CourseView
    {
        private readonly ICourseRepository _courseRepository;

        public CourseView(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task ShowAllCourses()
        {
            var courses = await _courseRepository.GetAll();

            Console.WriteLine("\n--- Courses ---");
            foreach (var course in courses)
            {
                Console.WriteLine($"Id: {course.Id}, Title: {course.Title}, Capacity: {course.CurrentCapacity}/{course.MaxCapacity}");
            }
        }

        public async Task AddCourse()
        {
            Console.Write("Enter Course Title: ");
            var title = Console.ReadLine();

            Console.Write("Enter Max Capacity: ");
            if (!int.TryParse(Console.ReadLine(), out var max))
            {
                Console.WriteLine("Invalid number");
                return;
            }

            var course = new UniversityManagement.Models.Course
            {
                Title = title ?? string.Empty,
                MaxCapacity = max,
                CurrentCapacity = 0
            };

            try
            {
                await _courseRepository.Add(course);
                Console.WriteLine("Course added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding course: {ex.Message}");
            }
        }
    }
}