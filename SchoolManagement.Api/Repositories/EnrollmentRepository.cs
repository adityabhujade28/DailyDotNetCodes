using Microsoft.EntityFrameworkCore;
using SchoolManagement.Api.Data;
using SchoolManagement.Api.Interfaces;
using SchoolManagement.Api.Models;

namespace SchoolManagement.Api.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
        => await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<List<Enrollment>> GetByStudentIdAsync(int studentId)
        => await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .Where(e => e.StudentId == studentId)
            .ToListAsync();

    public async Task<List<Enrollment>> GetByCourseIdAsync(int courseId)
        => await _context.Enrollments
            .Include(e => e.Student)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();

    public async Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId)
        => await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

    public async Task AddAsync(Enrollment enrollment)
        => await _context.Enrollments.AddAsync(enrollment);

    public void Update(Enrollment enrollment)
        => _context.Enrollments.Update(enrollment);

    public void Delete(Enrollment enrollment)
        => _context.Enrollments.Remove(enrollment);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}