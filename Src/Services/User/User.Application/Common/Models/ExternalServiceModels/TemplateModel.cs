namespace User.Application.Common.Models.ExternalServiceModels;

public sealed record TemplateModel
{
    /// <summary>
    /// شناسه
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; init; }
}
