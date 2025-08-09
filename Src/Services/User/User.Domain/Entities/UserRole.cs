using Microsoft.AspNetCore.Identity;

namespace User.Domain.Entities;

/// <summary>
/// نقش کاربران
/// </summary>
public sealed class UserRole : IdentityUserRole<int> 
{
    /// <summary>
    /// نقش
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// کاربر
    /// </summary>
    public UserAccount UserAccount { get; set; }
}
