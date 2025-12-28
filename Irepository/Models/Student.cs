namespace StudentCourseManagement.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}
