using Spectre.Console;
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
                Console.WriteLine("3. View Student by ID");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Search Students");
                Console.WriteLine("7. View Students (paged)");
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
                    case "3":
                        ViewStudentById();
                        break;
                    case "4":
                        UpdateStudent();
                        break;
                    case "5":
                        DeleteStudent();
                        break;
                    case "6":
                        SearchStudents();
                        break;
                    case "7":
                        ViewStudentsPaged();
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

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Email");
            table.AddColumn("Created");

            foreach (var student in students)
            {
                table.AddRow(student.StudentId.ToString(), student.StudentName, student.Email, student.CreatedDate.ToString("yyyy-MM-dd"));
            }

            AnsiConsole.Write(new Panel(table).Header("STUDENT LIST"));
        }

        private void ViewStudentById()
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID");
                return;
            }

            var student = _studentService.GetStudentById(studentId);
            if (student == null)
            {
                AnsiConsole.MarkupLine("[red]Student not found[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Field");
            table.AddColumn("Value");
            table.AddRow("ID", student.StudentId.ToString());
            table.AddRow("Name", student.StudentName);
            table.AddRow("Email", student.Email);
            table.AddRow("Created", student.CreatedDate.ToString("yyyy-MM-dd"));

            AnsiConsole.Write(new Panel(table).Header("STUDENT DETAILS"));
        }

        private void UpdateStudent()
        {
            Console.Write("Enter Student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID");
                return;
            }

            var student = _studentService.GetStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found");
                return;
            }

            Console.Write($"Enter new name (current: {student.StudentName}): ");
            var name = Console.ReadLine();

            Console.Write($"Enter new email (current: {student.Email}): ");
            var email = Console.ReadLine();

            try
            {
                _studentService.UpdateStudent(studentId, name ?? student.StudentName, email ?? student.Email);
                AnsiConsole.MarkupLine("[green]Student updated successfully.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        private void DeleteStudent()
        {
            Console.Write("Enter Student ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid Student ID");
                return;
            }

            Console.Write("Are you sure you want to delete this student? (y/n): ");
            var confirm = Console.ReadLine();
            if (!string.Equals(confirm, "y", StringComparison.OrdinalIgnoreCase))
            {
                AnsiConsole.MarkupLine("[yellow]Delete cancelled.[/]");
                return;
            }

            try
            {
                _studentService.DeleteStudent(studentId);
                AnsiConsole.MarkupLine("[green]Student deleted successfully.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        private void SearchStudents()
        {
            Console.Write("Enter search term (name or email): ");
            var q = Console.ReadLine();
            var results = _studentService.SearchStudents(q ?? string.Empty);

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Email");

            foreach (var s in results)
            {
                table.AddRow(s.StudentId.ToString(), s.StudentName, s.Email);
            }

            AnsiConsole.Write(new Panel(table).Header("SEARCH RESULTS"));
        }

        private void ViewStudentsPaged()
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

            var students = _studentService.GetStudentsPaged(page, pageSize);

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Email");

            foreach (var student in students)
            {
                table.AddRow(student.StudentId.ToString(), student.StudentName, student.Email);
            }

            AnsiConsole.Write(new Panel(table).Header($"STUDENT LIST (Page {page})"));
        }
    }
}
