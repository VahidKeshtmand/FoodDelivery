namespace User.Application.Options;

/// <summary>
/// تنظیمات Identity
/// </summary>
public sealed record IdentitySettingsOptions
{
    /// <summary>
    /// آیا رمزعبور باید عدد داشته باشد؟
    /// </summary>
    public bool PasswordRequireDigit { get; init; }

    /// <summary>
    /// طول رمزعبور
    /// </summary>
    public int PasswordRequiredLength { get; init; }

    /// <summary>
    /// آیا رمز عبور باید حروفی مثل @#$ داشته باشد؟
    /// </summary>
    public bool PasswordRequireNonAlphanumeric { get; init; }

    /// <summary>
    /// آیا حتما باید کاراکتری با حروف بزرگ داشته باشد؟
    /// </summary>
    public bool PasswordRequireUppercase { get; init; }

    /// <summary>
    /// آیا حتما باید کاراکتری با حروف کوچک داشته باشد؟
    /// </summary>
    public bool PasswordRequireLowercase { get; init; }

    /// <summary>
    /// آیا باید ایمیل یکتا باشد؟
    /// </summary>
    public bool RequireUniqueEmail { get; init; }
}
