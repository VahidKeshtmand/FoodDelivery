using User.Domain.Common;

namespace User.Domain.Entities;

/// <summary>
/// توکن کاربر
/// </summary>
public sealed class RefreshToken : BaseEntity
{
    /// <summary>
    /// رفرش توکن
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// تاریخ انقضای رفرش توکن
    /// </summary>
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// آیا رفرش توکن قبلا استفاده شده؟
    /// </summary>
    public bool Used { get; set; }

    /// <summary>
    /// شناسه کاربر
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// توکن استفاده شده
    /// </summary>
    public void UseToken() {
        Used = true;
    }
}
