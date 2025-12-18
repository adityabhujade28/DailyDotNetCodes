using StudentCourseEnrollmentSystem.Views;

namespace StudentCourseEnrollmentSystem.Views
{
    public class MenuView
    {
        private readonly StudentView _studentView;
        private readonly CourseView _courseView;
        private readonly EnrollmentView _enrollmentView;

        public MenuView(
            StudentView studentView,
            CourseView courseView,
            EnrollmentView enrollmentView)
        {
            _studentView = studentView;
            _courseView = courseView;
            _enrollmentView = enrollmentView;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n===== STUDENT COURSE ENROLLMENT SYSTEM =====");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Course Management");
                Console.WriteLine("3. Enrollment Management");
                Console.WriteLine("0. Exit");

                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _studentView.ShowMenu();
                        break;
                    case "2":
                        _courseView.ShowMenu();
                        break;
                    case "3":
                        _enrollmentView.ShowMenu();
                        break;
                    case "0":
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
