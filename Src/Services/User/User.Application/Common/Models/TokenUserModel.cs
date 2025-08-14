using User.Application.Common.Models.BaseDtos;
using User.Domain.Entities;

namespace User.Application.Common.Models;

/// <summary>
/// مدل کاربر برای ساخت توکن
/// </summary>
public sealed record TokenUserModel
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// ایمیل
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// نقش ها
    /// </summary>
    public List<string> Roles { get; set; } = new();
}
