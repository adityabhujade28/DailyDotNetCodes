using System.Linq;
using StudentCourseManagement.Models;
using StudentCourseManagement.Services;

namespace StudentCourseManagement.Views;

public class StudentView
{
    private readonly StudentService _studentService;

    public StudentView(StudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task ShowActiveStudentsAsync()
    {
        var students = (await _studentService.GetActiveStudentsAsync()).ToList();
        if (!students.Any())
        {
            Console.WriteLine("No active students found.");
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id} - {student.Name}");
        }
    }
}
