using Spectre.Console;
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
                Console.WriteLine("3. View Course by ID");
                Console.WriteLine("4. Update Course");
                Console.WriteLine("5. Delete Course");
                Console.WriteLine("6. Search Courses");
                Console.WriteLine("7. View Courses (paged)");
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
                    case "3":
                        ViewCourseById();
                        break;
                    case "4":
                        UpdateCourse();
                        break;
                    case "5":
                        DeleteCourse();
                        break;
                    case "6":
                        SearchCourses();
                        break;
                    case "7":
                        ViewCoursesPaged();
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

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Credits");
            table.AddColumn("Created");

            foreach (var course in courses)
            {
                table.AddRow(course.CourseId.ToString(), course.CourseName, course.Credits.ToString(), course.CreatedDate.ToString("yyyy-MM-dd"));
            }

            AnsiConsole.Write(new Panel(table).Header("COURSE LIST"));
        }

        private void ViewCourseById()
        {
            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID");
                return;
            }

            var course = _courseService.GetCourseById(courseId);
            if (course == null)
            {
                AnsiConsole.MarkupLine("[red]Course not found[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Field");
            table.AddColumn("Value");
            table.AddRow("ID", course.CourseId.ToString());
            table.AddRow("Name", course.CourseName);
            table.AddRow("Credits", course.Credits.ToString());
            table.AddRow("Created", course.CreatedDate.ToString("yyyy-MM-dd"));

            AnsiConsole.Write(new Panel(table).Header("COURSE DETAILS"));
        }

        private void UpdateCourse()
        {
            Console.Write("Enter Course ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID");
                return;
            }

            var course = _courseService.GetCourseById(courseId);
            if (course == null)
            {
                Console.WriteLine("Course not found");
                return;
            }

            Console.Write($"Enter new name (current: {course.CourseName}): ");
            var name = Console.ReadLine();

            Console.Write($"Enter new credits (current: {course.Credits}): ");
            var creditsInput = Console.ReadLine();
            if (!int.TryParse(creditsInput, out int credits))
            {
                Console.WriteLine("Invalid credits.");
                return;
            }

            try
            {
                _courseService.UpdateCourse(courseId, name ?? course.CourseName, credits);
                Console.WriteLine("Course updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void DeleteCourse()
        {
            Console.Write("Enter Course ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID");
                return;
            }

            Console.Write("Are you sure you want to delete this course? (y/n): ");
            var confirm = Console.ReadLine();
            if (!string.Equals(confirm, "y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            try
            {
                _courseService.DeleteCourse(courseId);
                Console.WriteLine("Course deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void SearchCourses()
        {
            Console.Write("Enter search term (course name): ");
            var q = Console.ReadLine();
            var results = _courseService.SearchCourses(q ?? string.Empty);

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Credits");

            foreach (var c in results)
            {
                table.AddRow(c.CourseId.ToString(), c.CourseName, c.Credits.ToString());
            }

            AnsiConsole.Write(new Panel(table).Header("SEARCH RESULTS"));
        }

        private void ViewCoursesPaged()
        {
            Console.Write("Enter page number: ");
            if (!int.TryParse(Console.ReadLine(), out int page) || page <= 0)
            {
                Console.WriteLine("Invalid page number.");
                return;
            }

            Console.Write("Enter page size: ");
            if (!int.TryParse(Console.ReadLine(), out int pageSize) || pageSize <= 0)
            {
                Console.WriteLine("Invalid page size.");
                return;
            }

            var courses = _courseService.GetCoursesPaged(page, pageSize);

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Credits");

            foreach (var course in courses)
            {
                table.AddRow(course.CourseId.ToString(), course.CourseName, course.Credits.ToString());
            }

            AnsiConsole.Write(new Panel(table).Header($"COURSE LIST (Page {page})"));
        }
    }
}
