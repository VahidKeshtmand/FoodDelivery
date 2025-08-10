namespace User.Application.Common.Exceptions;

/// <summary>
/// CustomException برای عدم احراز هویت
/// </summary>
public class UnAuthorizedException : Exception
{
    public UnAuthorizedException() : base("عدم احراز هویت") {

    }

    public UnAuthorizedException(string message) : base(message) {

    }
}
