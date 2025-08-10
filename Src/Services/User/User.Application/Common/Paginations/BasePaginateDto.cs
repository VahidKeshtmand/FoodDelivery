namespace User.Application.Common.Paginations;

/// <summary>
/// دیتای پایه pagination
/// </summary>
public record BasePaginateDto
{
    /// <summary>
    /// شماره صفحه
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// تعداد رکورد هر صفحه
    /// </summary>
    public int PageSize { get; init; } = 10;
}
