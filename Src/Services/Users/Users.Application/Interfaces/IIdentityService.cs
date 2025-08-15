using System.Security.Claims;
using Users.Application.Common.Models;

namespace Users.Application.Interfaces;

public interface IIdentityService
{
    int GetUserId();

    TokenDataModel GenerateTokenAsync(TokenUserModel user, string refreshToken);

    bool IsRoleExists(string role);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}
