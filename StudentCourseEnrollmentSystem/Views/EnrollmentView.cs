using Spectre.Console;
using StudentCourseEnrollmentSystem.Interfaces;

namespace StudentCourseEnrollmentSystem.Views
{
    public class EnrollmentView
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollmentView(IEnrollmentService enrollmentService, IStudentService studentService, ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- ENROLLMENT MENU ---");
                Console.WriteLine("1. Enroll Student");
                Console.WriteLine("2. View All Enrollments");
                Console.WriteLine("3. View Enrollments by Student");
                Console.WriteLine("4. View Enrollments by Course");
                Console.WriteLine("5. Drop (Unenroll) Student from Course");
                Console.WriteLine("6. View Top Performers");
                Console.WriteLine("7. View Course Statistics");
                Console.WriteLine("0. Back");

                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnrollStudent();
                        break;
                    case "2":
                        ViewAllEnrollments();
                        break;
                    case "3":
                        ViewByStudent();
                        break;
                    case "4":
                        ViewByCourse();
                        break;
                    case "5":
                        DropCourse();
                        break;
                    case "6":
                        ViewTopPerformers();
                        break;
                    case "7":
                        ViewCourseStatistics();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void EnrollStudent()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID");
                return;
            }

            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID");
                return;
            }

            Console.Write("Enter Grade (optional, press Enter to skip): ");
            var gradeInput = Console.ReadLine();

            try
            {
                if (string.IsNullOrWhiteSpace(gradeInput))
                {
                    _enrollmentService.EnrollStudent(studentId, courseId);
                }
                else if (decimal.TryParse(gradeInput, out decimal grade))
                {
                    _enrollmentService.EnrollStudent(studentId, courseId, grade);
                }
                else
                {
                    Console.WriteLine("Invalid grade format");
                    return;
                }

                Console.WriteLine("Student enrolled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ViewAllEnrollments()
        {
            var enrollments = _enrollmentService.GetEnrollments();

            var table = new Table();
            table.AddColumn("Student");
            table.AddColumn("Course");
            table.AddColumn("Grade");
            table.AddColumn("Status");

            foreach (var e in enrollments)
            {
                table.AddRow(e.StudentName, e.CourseName, (e.Grade ?? 0).ToString(), e.Status);
            }

            AnsiConsole.Write(new Panel(table).Header("ALL ENROLLMENTS"));
        }

        private void ViewByStudent()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID");
                return;
            }

            var enrollments = _enrollmentService.GetEnrollmentsByStudent(studentId);

            var table = new Table();
            table.AddColumn("Course");
            table.AddColumn("Grade");
            table.AddColumn("Status");

            foreach (var e in enrollments)
            {
                table.AddRow(e.CourseName, (e.Grade ?? 0).ToString(), e.Status);
            }

            AnsiConsole.Write(new Panel(table).Header("ENROLLMENTS BY STUDENT"));
        }

        private void ViewByCourse()
        {
            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID");
                return;
            }

            var enrollments = _enrollmentService.GetEnrollmentsByCourse(courseId);

            var table = new Table();
            table.AddColumn("Student");
            table.AddColumn("Grade");
            table.AddColumn("Status");

            foreach (var e in enrollments)
            {
                table.AddRow(e.StudentName, (e.Grade ?? 0).ToString(), e.Status);
            }

            AnsiConsole.Write(new Panel(table).Header("ENROLLMENTS BY COURSE"));
        }

        private void DropCourse()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID");
                return;
            }

            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID");
                return;
            }

            try
            {
                _enrollmentService.UnenrollStudent(studentId, courseId);
                Console.WriteLine("Student unenrolled (dropped) successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ViewTopPerformers()
        {
            Console.Write("How many top performers to show? ");
            if (!int.TryParse(Console.ReadLine(), out int topN) || topN <= 0)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            var performers = _studentService.GetTopPerformers(topN);

            var table = new Table();
            table.AddColumn("Student (ID)");
            table.AddColumn("Avg Grade");
            table.AddColumn("Courses");

            foreach (var p in performers)
            {
                table.AddRow($"{p.StudentName} ({p.StudentId})", p.AverageGrade.ToString(), p.CourseCount.ToString());
            }

            AnsiConsole.Write(new Panel(table).Header("TOP PERFORMERS"));
        }

        private void ViewCourseStatistics()
        {
            var stats = _courseService.GetCourseStatistics();

            var table = new Table();
            table.AddColumn("Course (ID)");
            table.AddColumn("Enrollments");
            table.AddColumn("Avg Grade");

            foreach (var c in stats)
            {
                var avg = c.AverageGrade.HasValue ? c.AverageGrade.Value.ToString() : "N/A";
                table.AddRow($"{c.CourseName} ({c.CourseId})", c.EnrollmentCount.ToString(), avg);
            }

            AnsiConsole.Write(new Panel(table).Header("COURSE STATISTICS"));
        }
    }
}
