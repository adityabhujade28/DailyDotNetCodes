using UniversityManagement.Views;

namespace UniversityManagement.Views
{
    public class MainMenuView
    {
        private readonly StudentView _studentView;
        private readonly EnrollmentView _enrollmentView;
        private readonly ReportingView _reportingView;
        private readonly CourseView _courseView;
        private readonly DepartmentView _departmentView;

        public MainMenuView(
            StudentView studentView,
            EnrollmentView enrollmentView,
            ReportingView reportingView,
            CourseView courseView,
            DepartmentView departmentView)
        {
            _studentView = studentView;
            _enrollmentView = enrollmentView;
            _reportingView = reportingView;
            _courseView = courseView;
            _departmentView = departmentView;
        }

        public async Task ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== University Management System =====");
                Console.WriteLine("1. View Students");
                Console.WriteLine("2. Enroll Student in Course");
                Console.WriteLine("3. View Student Courses");
                Console.WriteLine("4. Department Report");
                Console.WriteLine("5. Add Student");
                Console.WriteLine("6. Add Course");
                Console.WriteLine("7. View Courses");
                Console.WriteLine("8. Add Department");
                Console.WriteLine("9. Assign Student to Department");
                Console.WriteLine("0. Exit");

                Console.Write("Select option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await _studentView.ShowAllStudents();
                        break;

                    case "2":
                        await _enrollmentView.EnrollStudent();
                        break;

                    case "3":
                        await _enrollmentView.ViewStudentCourses();
                        break;

                    case "4":
                        await _reportingView.DepartmentReport();
                        break;

                    case "5":
                        await _studentView.AddStudent();
                        break;

                    case "6":
                        await _courseView.AddCourse();
                        break;

                    case "7":
                        await _courseView.ShowAllCourses();
                        break;

                    case "8":
                        await _departmentView.AddDepartment();
                        break;

                    case "9":
                        await _studentView.UpdateStudentDepartment();
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
