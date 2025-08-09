//using User.Application.Common.Models;
using System.Security.Claims;
using System.Text;

namespace User.Application.Interfaces;

///// <summary>
///// سرویس بدست آوردن اطلاعات کاربر لاگین شده
///// </summary>
//public interface IIdentityService
//{
//    /// <summary>
//    /// بدست آوردن شناسه کاربر لاگین شده
//    /// </summary>
//    /// <returns></returns>
//    int GetUserId();

//    /// <summary>
//    /// ساخت توکن
//    /// </summary>
//    /// <param name="user">کاربر</param>
//    /// <returns></returns>
//    TokenDataModel GenerateTokenAsync(TokenUserModel user, string refreshToken);

//    /// <summary>
//    /// آیا این نقش وجود دارد؟
//    /// </summary>
//    /// <param name="role"></param>
//    /// <returns></returns>
//    bool IsRoleExists(string role);

//    /// <summary>
//    /// اعتبارسنجی توکن و بدست آوردن claim ها
//    /// </summary>
//    /// <param name="token"></param>
//    /// <returns></returns>
//    ClaimsPrincipal GetPrincipalFromToken(string token);
//}
