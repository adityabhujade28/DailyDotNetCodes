namespace SchoolManagement.Api.DTOs;

public class CourseQueryParameters
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

    public string? Title { get; set; }
    public int? DepartmentId { get; set; }
    public string? Search { get; set; }

    // Sorting: title, credits, createdAt
    public string? SortBy { get; set; }
    public string? SortDir { get; set; } // "asc" or "desc"
}