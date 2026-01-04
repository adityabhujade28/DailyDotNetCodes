namespace SchoolManagement.Api.DTOs;

/// <summary>
/// Generic paged result
/// </summary>
/// <typeparam name="T">Item type</typeparam>
public class PagedResult<T>
{
    /// <summary>Page items</summary>
    public List<T> Items { get; set; } = new List<T>();

    /// <summary>Current page number</summary>
    public int Page { get; set; }

    /// <summary>Page size</summary>
    public int PageSize { get; set; }

    /// <summary>Total number of items</summary>
    public int TotalCount { get; set; }

    /// <summary>Total pages</summary>
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
}