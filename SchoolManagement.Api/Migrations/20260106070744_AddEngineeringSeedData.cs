using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEngineeringSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Engineering" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Credits", "DepartmentId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 3, 1, "Fundamentals of computer engineering", "Introduction to Computer Engineering" },
                    { 2, 4, 1, "Core data structures and algorithm analysis", "Data Structures and Algorithms" },
                    { 3, 3, 1, "Combinational and sequential circuits", "Digital Logic Design" },
                    { 4, 3, 1, "Continuous and discrete signal analysis", "Signals and Systems" },
                    { 5, 3, 1, "Networking principles and protocols", "Computer Networks" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "CreatedAt", "DateOfBirth", "DepartmentId", "Email", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "aarav.sharma@eng.example.edu", "Aarav Sharma", null, null },
                    { 2, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "priya.verma@eng.example.edu", "Priya Verma", null, null },
                    { 3, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "rohit.gupta@eng.example.edu", "Rohit Gupta", null, null },
                    { 4, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "neha.singh@eng.example.edu", "Neha Singh", null, null },
                    { 5, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2003, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "vikram.patel@eng.example.edu", "Vikram Patel", null, null },
                    { 6, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "sagar.kulkarni@eng.example.edu", "Sagar Kulkarni", null, null },
                    { 7, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "sneha.reddy@eng.example.edu", "Sneha Reddy", null, null },
                    { 8, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "manish.kumar@eng.example.edu", "Manish Kumar", null, null },
                    { 9, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2003, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "anika.kapoor@eng.example.edu", "Anika Kapoor", null, null },
                    { 10, null, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "karan.mehta@eng.example.edu", "Karan Mehta", null, null }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrollmentDate", "Grade", "NumericGrade", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", 4.00m, 1 },
                    { 2, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A-", 3.70m, 1 },
                    { 3, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B+", 3.30m, 2 },
                    { 4, 3, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", 3.00m, 3 },
                    { 5, 4, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", 4.00m, 4 },
                    { 6, 5, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B-", 2.70m, 5 },
                    { 7, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", 4.00m, 6 },
                    { 8, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B+", 3.30m, 7 },
                    { 9, 3, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A-", 3.70m, 8 },
                    { 10, 4, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", 4.00m, 9 },
                    { 11, 5, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", 3.00m, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
