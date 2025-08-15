namespace Users.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an operation is attempted without sufficient permissions.
/// Used to indicate forbidden access scenarios in the application.
/// </summary>
public sealed class ForbiddenException : Exception
{
    public ForbiddenException() : base("Access is denied due to insufficient permissions.") { }

    public ForbiddenException(string message) : base(message) {

    }
}
