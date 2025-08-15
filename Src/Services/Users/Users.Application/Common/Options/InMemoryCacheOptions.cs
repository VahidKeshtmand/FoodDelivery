namespace Users.Application.Common.Options;

public sealed record InMemoryCacheOptions
{
    public int AbsoluteExpirationSeconds { get; init; }

    public int SlidingExpirationSeconds { get; init; }

}
