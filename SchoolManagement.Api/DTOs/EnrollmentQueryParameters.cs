namespace SchoolManagement.Api.DTOs;

public class EnrollmentQueryParameters
{
    private int _page = 1;
    private int _pageSize = 10;
    private const int MaxPageSize = 100;

    public int Page
    {
        get => _page;
        set => _page = (value < 1) ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value < 1) ? 10 : (value > MaxPageSize ? MaxPageSize : value);
    }

    public int? StudentId { get; set; }
    public int? CourseId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    // Sorting: enrollmentDate, numericGrade
    public string? SortBy { get; set; }
    public string? SortDir { get; set; } // "asc" or "desc"
}