namespace Users.Application.Common.Options;

public sealed class DatabaseOptions
{
    public ConnectionStrings ConnectionStrings { get; init; } = new();

    public bool UseInMemoryDatabase { get; init; } = false;
}

public sealed class ConnectionStrings 
{
    public string AppDbContext { get; init; } = "";
}
