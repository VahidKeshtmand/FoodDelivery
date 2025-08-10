using Microsoft.EntityFrameworkCore;

namespace User.Application.Common.Paginations;

/// <summary>
/// کلاس کمکی pagination
/// </summary>
public static class PaginatedExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
}
