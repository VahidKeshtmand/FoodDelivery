namespace Users.Application.Common.Models;

public sealed record TokenUserModel
{
    public int Id { get; set; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public List<string> Roles { get; set; } = [];
}
