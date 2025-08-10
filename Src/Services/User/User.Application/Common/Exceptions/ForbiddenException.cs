namespace User.Application.Common.Exceptions;

/// <summary>
/// CustomException برای عدم دسترسی
/// </summary>
public class ForbiddenException : Exception 
{
    public ForbiddenException() : base("عدم دسترسی") {

    }

    public ForbiddenException(string message) : base(message) {

    }
}
