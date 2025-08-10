namespace User.Application.Common.Options;

/// <summary>
/// تنظیمات مربوط به MemoryCache
/// </summary>
public sealed record InMemoryCacheOptions
{
    /// <summary>
    /// AbsoluteExpirationSeconds
    /// </summary>
    public int AbsoluteExpirationSeconds { get; init; }

    /// <summary>
    /// SlidingExpirationSeconds
    /// </summary>
    public int SlidingExpirationSeconds { get; init; }

}
