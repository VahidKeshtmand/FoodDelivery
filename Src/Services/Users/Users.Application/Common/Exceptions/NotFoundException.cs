namespace Users.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested entity or record cannot be found in the data store.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException() : base("The requested resource was not found.") { }

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string entityName, int id)
        : base($"The entity '{entityName}' with ID '{id}' was not found.") { }
}
