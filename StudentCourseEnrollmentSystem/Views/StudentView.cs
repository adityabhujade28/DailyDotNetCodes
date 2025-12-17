using StudentCourseEnrollmentSystem.Interfaces;

namespace StudentCourseEnrollmentSystem.Views
{
    public class StudentView
    {
        private readonly IStudentService _studentService;

        public StudentView(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- STUDENT MENU ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("0. Back");

                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Write("Enter student name: ");
            var name = Console.ReadLine();

            Console.Write("Enter email: ");
            var email = Console.ReadLine();

            _studentService.AddStudent(name!, email!);
            Console.WriteLine("Student added successfully.");
        }

        private void ViewStudents()
        {
            var students = _studentService.GetAllStudents();

            Console.WriteLine("\n--- STUDENT LIST ---");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId} - {student.StudentName} ({student.Email})");
            }
        }
    }
}
