using StudentCourseEnrollmentSystem.Interfaces;

namespace StudentCourseEnrollmentSystem.Views
{
    public class CourseView
    {
        private readonly ICourseService _courseService;

        public CourseView(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- COURSE MENU ---");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View All Courses");
                Console.WriteLine("0. Back");

                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCourse();
                        break;
                    case "2":
                        ViewCourses();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void AddCourse()
        {
            Console.Write("Enter course name: ");
            var name = Console.ReadLine();

            Console.Write("Enter credits: ");
            var creditsInput = Console.ReadLine();

            if (!int.TryParse(creditsInput, out int credits))
            {
                Console.WriteLine("Invalid credits.");
                return;
            }

            _courseService.AddCourse(name!, credits);
            Console.WriteLine("Course added successfully.");
        }

        private void ViewCourses()
        {
            var courses = _courseService.GetAllCourses();

            Console.WriteLine("\n--- COURSE LIST ---");
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.CourseId} - {course.CourseName} (Credits: {course.Credits})");
            }
        }
    }
}
