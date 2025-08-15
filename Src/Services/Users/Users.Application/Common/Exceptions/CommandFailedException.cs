using Users.Application.Common.Models;

namespace Users.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a command fails to execute successfully.
/// Can include additional error details or validation errors to provide more context about the failure.
/// </summary>
public sealed class CommandFailedException : Exception
{
    public IReadOnlyDictionary<string, string>? Values { get; }
    public IReadOnlyList<ValidationErrorModel>? Errors { get; }

    public CommandFailedException() : base("The command failed to execute due to one or more errors.") { }

    public CommandFailedException(string message) : base(message) { }

    public CommandFailedException(Dictionary<string, string> values) : base("The command failed to execute due to one or more errors.") {
        Values = values;
    }

    public CommandFailedException(Dictionary<string, string> values, string message) : base(message) {
        Values = values;
    }

    public CommandFailedException(List<ValidationErrorModel> error)
        : base("The command failed to execute due to one or more validation errors.") {
        Errors = error;
    }
}
