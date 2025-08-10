namespace User.Application.Options;

/// <summary>
/// تنظیمات پایگاه داده
/// </summary>
public sealed class DatabaseOptions
{
    /// <summary>
    /// رشته اتصال به پایگاه داده
    /// </summary>
    public ConnectionStrings ConnectionStrings { get; init; } = new();

    /// <summary>
    /// آیا از پایگاه داده حالت InMemory استفاده بشود؟
    /// </summary>
    public bool UseInMemoryDatabase { get; init; } = false;
}

/// <summary>
/// رشته های اتصال به پایگاه داده
/// </summary>
public sealed class ConnectionStrings 
{
    /// <summary>
    /// رشته اتصال مربوط به AppDbContext
    /// </summary>
    public string AppDbContext { get; init; } = "";
}
