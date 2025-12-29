using UniversityManagement.Interfaces;

namespace UniversityManagement.Views
{
    public class StudentView
    {
        private readonly IStudentRepository _studentRepository;

        public StudentView(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task ShowAllStudents()
        {
            var students = await _studentRepository.GetAll();

            Console.WriteLine("\n--- Students ---");
            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, GPA: {student.GPA}");
            }
        }

        public async Task AddStudent()
        {
            Console.Write("Enter Student Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter GPA (e.g. 3.5): ");
            if (!double.TryParse(Console.ReadLine(), out var gpa))
            {
                Console.WriteLine("Invalid GPA");
                return;
            }

            Console.Write("Enter Department Id (or blank): ");
            var deptInput = Console.ReadLine();
            int? deptId = null;
            if (!string.IsNullOrWhiteSpace(deptInput) && int.TryParse(deptInput, out var parsed))
                deptId = parsed;

            var student = new UniversityManagement.Models.Student
            {
                Name = name ?? string.Empty,
                GPA = gpa,
                DepartmentId = deptId
            };

            try
            {
                await _studentRepository.Add(student);
                Console.WriteLine("Student added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student: {ex.Message}");
            }
        }

        public async Task UpdateStudentDepartment()
        {
            Console.Write("Enter Student Id: ");
            if (!int.TryParse(Console.ReadLine(), out var studentId))
            {
                Console.WriteLine("Invalid Student Id");
                return;
            }

            var student = await _studentRepository.GetById(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found");
                return;
            }

            Console.Write("Enter Department Id (or blank to remove): ");
            var deptInput = Console.ReadLine();
            int? deptId = null;
            if (!string.IsNullOrWhiteSpace(deptInput) && int.TryParse(deptInput, out var parsed))
                deptId = parsed;

            student.DepartmentId = deptId;

            try
            {
                await _studentRepository.Update(student);
                Console.WriteLine("Student department updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student: {ex.Message}");
            }
        }
    }
}
