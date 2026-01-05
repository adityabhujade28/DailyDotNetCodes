using System.Linq.Expressions;

namespace SchoolManagement.Api.Utilities;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return source.Skip(skip).Take(pageSize);
    }

    public static IOrderedQueryable<T> OrderByDirection<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector, string? sortDir)
    {
        var dir = sortDir?.ToLower() == "desc";
        return dir ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }
}