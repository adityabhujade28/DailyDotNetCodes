using Pagination.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pagination.Models;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        context.Database.EnsureCreated();

        SeedData(context);

        Console.WriteLine("===== INCLUDE Queries =====");
        ReadWithInclude(context);

        Console.WriteLine("\n===== AsNoTracking Queries =====");
        ReadWithNoTracking(context);

        Console.WriteLine("\n===== LINQ QUERIES =====");
        LinqQueries(context);

        Console.WriteLine("\n===== PAGINATION =====");
        Pagination(context);

        Console.WriteLine("\n===== UPDATE (DISCONNECTED) =====");
        UpdateStudent(context);

        Console.WriteLine("\n===== DELETE =====");
        DeleteStudent(context);

        Console.WriteLine("\n===== LINQ PRACTICE =====");
        PracticeLinq(context);
    }

    // ---------------- METHODS ----------------


    static void PracticeLinq(AppDbContext context)
    {
        //Students enrolled in "C#" course with grade > 3.5
        //var StudentDetails = context.Enrollments
        //    .Where(e => e.Grade > 3.5)
        //    .Select(e => new
        //    {
        //        StudentName = e.Student.Name,
        //        StudentId = e.Student.StudentId,
        //        StudentGrade = e.Grade,
        //    });
        //foreach (var s in StudentDetails)
        //{
        //    Console.WriteLine($"{s.StudentId} - {s.StudentName} - {s.StudentGrade}");
        //}


        // GET ONLY STUDENT NAMES IN ORDER.
        //var StudentName = context.Students
        //    .AsNoTracking()
        //    .OrderBy(s => s.Name)
        //    .ToList();

        //foreach (var Name in StudentName)
        //{
        //    Console.WriteLine($"Student name : {Name.Name}");
        //}
        ////Console.WriteLine(StudentName);



        ////TOP 5 student in descending order, order by GPA
        //var Top5 = context.Students
        //    .OrderByDescending(e => e.GPA)
        //    .AsNoTracking()
        //    .Take(5)
        //    .Select(s =>  s.Name );

        //Console.WriteLine(Top5);

        //foreach(var s in Top5)
        //{
        //    Console.WriteLine($"{s}");
        //}

        //Total Number of Students.
        var TotalStudent = context.Students
                            .AsNoTracking()
                            .Count();

        Console.WriteLine($"TotalStudents = {TotalStudent}");


        //Get all Courses Whose Name Contains "C".
        //var Cnames = context.Courses
        //    .AsNoTracking()
        //    .Where(c => c.CourseName.Contains("C"))
        //    .Select(c => c.CourseName);

        //foreach (var c in Cnames)
        //{
        //    Console.WriteLine($"{c}");
        //}


        // Students name along with course count
        //var EnrollCount = context.Students
        //    .AsNoTracking()
        //    .Select(e => new
        //    {
        //        studentName = e.Name,
        //        courseCount = e.Enrollments.Count()
        //    });
        //foreach(var e in EnrollCount)
        //{
        //    Console.WriteLine($"{e.studentName} and course enrolled in {e.courseCount}");
        //}

        //Students Enrolled in C#

        //var enrollinC = context.Enrollments
        //    .Where(e => e.Course.CourseName == "C#")
        //    .Select(e => e.Student.Name);
        //foreach (var s in enrollinC)
        //{
        //    Console.WriteLine($"{s}");
        //}

        //Average grade per Course

        //var AvgInCourse = context.Enrollments
        //    .AsNoTracking()
        //    .GroupBy(c => c.Course.CourseName)
        //    .Select(c => new { Course = c.Key, AvgGrade = c.Average(m => m.Grade) });

        //foreach (var s in AvgInCourse)
        //{
        //    Console.WriteLine($"Course : {s.Course} Average : {s.AvgGrade}");
        //}

        //Pagination for Page Size of 3
        //int PageSize = 3;
        //int PageNo = 5;
        //for(int i = 1; i <= PageNo; i++)
        //{
        //    Console.WriteLine($"Page Number : {i}");
        //    var StudentDetails = context.Students
        //        .AsNoTracking()
        //        .OrderBy(s => s.StudentId)
        //        .Skip((i - 1) * PageSize)
        //        .Take(PageSize)
        //        .ToList();

        //    foreach(var s in StudentDetails)
        //    {
        //        Console.WriteLine($"{s.Name} --- {s.GPA} --- {s.StudentId}");
        //    }
        //}

        //Topper of every Course
        var toppers = context.Enrollments
            .AsNoTracking()
            .GroupBy(e => e.Course.CourseName)
            .Select(g => g
            .OrderByDescending(x => x.Grade)
            .Select(x => new
            {
                x.Course.CourseName,
                x.Student.Name,
                x.Grade,
            })
            .First()
            );

        foreach(var s in toppers)
        {
            Console.WriteLine($"{s.CourseName} and {s.Name} and {s.Grade}");
        }


    }

    static void SeedData(AppDbContext context)
    {
        if (context.Students.Any()) return;

        var courses = new[]
        {
        new Course { CourseName = "C#" },
        new Course { CourseName = "SQL" },
        new Course { CourseName = "ASP.NET" }
    };

        context.Courses.AddRange(courses);
        context.SaveChanges();

        var students = Enumerable.Range(1, 15)
            .Select(i => new Student
            {
                Name = $"Student {i}",
                GPA = Math.Round(2.5 + (i * 0.1), 2)
            })
            .ToList();

        context.Students.AddRange(students);
        context.SaveChanges();

        var enrollments = students.SelectMany(s => new[]
        {
        new Enrollment
        {
            StudentId = s.StudentId,
            CourseId = courses[0].CourseId,
            Grade = s.GPA
        },
        new Enrollment
        {
            StudentId = s.StudentId,
            CourseId = courses[1].CourseId,
            Grade = s.GPA - 0.2
        }
    });

        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }


    static void ReadWithInclude(AppDbContext context)
    {
        var students = context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .ToList();

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name}");
            foreach (var e in s.Enrollments)
                Console.WriteLine($"  {e.Course.CourseName} - {e.Grade}");
        }
    }

    static void ReadWithNoTracking(AppDbContext context)
    {
        var students = context.Students
            .AsNoTracking()
            .ToList();

        foreach (var s in students)
            Console.WriteLine($"{s.Name} - {s.GPA}");
    }

    static void LinqQueries(AppDbContext context)
    {
        var topStudents = context.Students
            .Where(s => s.GPA > 3.6)
            .Select(s => new
            {
                s.Name,
                s.GPA
            })
            .ToList();

        foreach (var s in topStudents)
            Console.WriteLine($"{s.Name} - {s.GPA}");

        var avgGrades = context.Enrollments
            .GroupBy(e => e.Course.CourseName)
            .Select(g => new
            {
                Course = g.Key,
                AvgGrade = g.Average(x => x.Grade)
            })
            .ToList();

        foreach (var g in avgGrades)
            Console.WriteLine($"{g.Course} Avg: {g.AvgGrade}");
    }

    static void Pagination(AppDbContext context)
    {
        int pageSize = 5;

        for (int page = 1; page <= 3; page++)
        {
            Console.WriteLine($"\n--- Page {page} ---");

            var students = context.Students
                .AsNoTracking()
                .OrderBy(s => s.StudentId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var s in students)
                Console.WriteLine($"{s.StudentId} - {s.Name} - {s.GPA}");
        }
    }


    static void UpdateStudent(AppDbContext _)
    {
        using var context = new AppDbContext();
        var student = new Student { StudentId = 1, GPA = 3.9 };
        context.Students.Attach(student);
        context.Entry(student).Property(s => s.GPA).IsModified = true;
        context.SaveChanges();
    }

    // App: safer delete that checks DB first
    static void DeleteStudent(AppDbContext context)
    {
        var student = context.Students.Find(2); // returns null if not present
        if (student == null)
        {
            Console.WriteLine("Student 2 not found — nothing to delete.");
            return;
        }
        context.Students.Remove(student);
        context.SaveChanges();
        Console.WriteLine("Student deleted");
    }
}
