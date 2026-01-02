using Microsoft.Extensions.Logging;
using StudentCourseEnrollmentSystem.Interfaces;
using StudentCourseEnrollmentSystem.Models;
using StudentCourseEnrollmentSystem.DTOs;

namespace StudentCourseEnrollmentSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository, IEnrollmentRepository enrollmentRepository, ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
        }

        public void AddCourse(string courseName, int credits)
        {
            var course = new Course
            {
                CourseName = courseName,
                Credits = credits
            };

            _courseRepository.Add(course);
            _logger.LogInformation("Course added: {name} (ID: {id})", courseName, course.CourseId);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public Course? GetCourseById(int courseId)
        {
            return _courseRepository.GetById(courseId);
        }

        public void UpdateCourse(int courseId, string courseName, int credits)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null) throw new Exception("Course not found");

            course.CourseName = courseName;
            course.Credits = credits;

            _courseRepository.Update(course);
            _logger.LogInformation("Course updated: {id}", courseId);
        }

        public void DeleteCourse(int courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null) throw new Exception("Course not found");

            _courseRepository.Delete(course);
            _logger.LogInformation("Course deleted: {id}", courseId);
        }

        public IEnumerable<CourseStatsDto> GetCourseStatistics()
        {
            var stats = _enrollmentRepository.GetAll()
                .Where(e => e.Course != null)
                .GroupBy(e => new { e.CourseId, e.Course!.CourseName })
                .Select(g => new CourseStatsDto
                {
                    CourseId = g.Key.CourseId,
                    CourseName = g.Key.CourseName,
                    EnrollmentCount = g.Count(),
                    AverageGrade = g.Any(x => x.Grade.HasValue) ? Math.Round(g.Where(x => x.Grade.HasValue).Average(x => x.Grade!.Value), 2) : (decimal?)null
                })
                .OrderByDescending(c => c.EnrollmentCount)
                .ToList();

            return stats;
        }

        public IEnumerable<Course> GetCoursesPaged(int page, int pageSize)
        {
            return _courseRepository.GetAllPaged(page, pageSize);
        }

        public IEnumerable<Course> SearchCourses(string query)
        {
            return _courseRepository.Search(query);
        }
    }
}
