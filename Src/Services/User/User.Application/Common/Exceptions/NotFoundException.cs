namespace User.Application.Common.Exceptions;

/// <summary>
/// CustomException برای پیدا نکردن رکورد
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException() : base("رکورد مورد نظر یافت نشد.") {

    }

    public NotFoundException(string message) : base(message) {

    }

    public NotFoundException(string entityName, int id) : base($"{entityName} با شناسه {id} یافت نشد.") {

    }  
}
