using StudentCourseManagement.Data;
using StudentCourseManagement.Interfaces;
using StudentCourseManagement.Models;

namespace StudentCourseManagement.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly AppDbContext _context;

    public StudentService(IStudentRepository studentRepository, AppDbContext context)
    {
        _studentRepository = studentRepository;
        _context = context;
    }

    public async Task AddStudentAsync(Student student)
    {
        await _studentRepository.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Student>> GetActiveStudentsAsync()
    {
        return await _studentRepository.GetActiveStudentsAsync();
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _studentRepository.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null) return false;
        _studentRepository.Delete(student);
        await _context.SaveChangesAsync();
        return true;
    }
}
