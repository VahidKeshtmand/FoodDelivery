namespace Users.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an operation is attempted without proper authorization.
/// </summary>
public sealed class UnAuthorizedException : Exception
{
    public UnAuthorizedException()
        : base("The request could not be completed because the user is not authorized.") { }

    public UnAuthorizedException(string message) : base(message) { }
}

