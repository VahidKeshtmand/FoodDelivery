using User.Application.Common.Models;

namespace User.Application.Common.Exceptions;

public class CommandFailedException : Exception
{
    public Dictionary<string, string>? Values { get; private set; }

    public List<ValidationErrorModel>? Errors { get; private set; }

    public CommandFailedException() : base("خطا در انجام عملیات") {

    }

    public CommandFailedException(string message) : base(message) {

    }

    public CommandFailedException(Dictionary<string, string> values) : base("خطا در انجام عملیات") {
        Values = values;
    }

    public CommandFailedException(Dictionary<string, string> values, string message) : base(message) {
        Values = values;
    }

    public CommandFailedException(List<ValidationErrorModel> error) : base("Validation Error Occurred") {
        Errors = error;
    }
}
