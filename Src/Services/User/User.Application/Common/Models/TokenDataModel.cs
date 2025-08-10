namespace User.Application.Common.Models;

/// <summary>
/// مدل دیتایی توکن
/// </summary>
public sealed record TokenDataModel
{
    /// <summary>
    /// مقدار توکن
    /// </summary>
    public string Toke { get; init; }

    /// <summary>
    /// RefreshToken
    /// </summary>
    public string RefreshToken { get; init; }

    /// <summary>
    /// نوع توکن
    /// </summary>
    public string TokenType { get; init; }

    /// <summary>
    /// زمان منقضی شدن توکن
    /// </summary>
    public DateTime ExpireTime { get; init; }
}
