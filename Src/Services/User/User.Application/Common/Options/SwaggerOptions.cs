namespace User.Application.Options;

public sealed record SwaggerOptions 
{
    /// <summary>
    /// عنوان داکیومنت Swagger
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// توضیحات داکیومنت Swagger
    /// </summary>
    public string Description { get; init; }
}
