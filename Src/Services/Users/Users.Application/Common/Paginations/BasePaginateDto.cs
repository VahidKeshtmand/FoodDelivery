namespace Users.Application.Common.Paginations;

public record BasePaginateDto
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
