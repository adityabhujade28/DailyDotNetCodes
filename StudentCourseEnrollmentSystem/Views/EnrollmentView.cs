using StudentCourseEnrollmentSystem.Interfaces;

namespace StudentCourseEnrollmentSystem.Views
{
    public class EnrollmentView
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentView(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
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

            Console.WriteLine("\n--- ALL ENROLLMENTS ---");
            foreach (var e in enrollments)
            {
                Console.WriteLine(
                    $"Student: {e.StudentName} | Course: {e.CourseName} | Grade: {e.Grade ?? 0} | Status: {e.Status}"
                );
            }
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

            Console.WriteLine("\n--- ENROLLMENTS BY STUDENT ---");
            foreach (var e in enrollments)
            {
                Console.WriteLine(
                    $"Course: {e.CourseName} | Grade: {e.Grade ?? 0} | Status: {e.Status}"
                );
            }
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

            Console.WriteLine("\n--- ENROLLMENTS BY COURSE ---");
            foreach (var e in enrollments)
            {
                Console.WriteLine(
                    $"Student: {e.StudentName} | Grade: {e.Grade ?? 0} | Status: {e.Status}"
                );
            }
        }
    }
}
