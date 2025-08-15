namespace Users.Application.Common.Models;

public sealed record TokenDataModel
{
    public string Toke { get; init; }

    public string RefreshToken { get; init; }

    public string TokenType { get; init; }

    public DateTime ExpireTime { get; init; }
}
