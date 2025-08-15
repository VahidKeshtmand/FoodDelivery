using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Api.Common.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurants.Api.Common.Services;

internal sealed class IdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtOptions _jwtOptions;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public IdentityService(
        IHttpContextAccessor httpContextAccessor,
        IOptions<JwtOptions> jwtOption,
        TokenValidationParameters tokenValidationParameters) {

        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOption.Value;

        tokenValidationParameters.TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.EncryptKey));
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        tokenValidationParameters.ValidAudience = _jwtOptions.Audience;
        tokenValidationParameters.ValidIssuer = _jwtOptions.Issuer;

        _tokenValidationParameters = tokenValidationParameters;
    }

    public int GetUserId() {
        return Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    public bool IsRoleExists(string role) {
        return _httpContextAccessor.HttpContext?.User
            ?.FindAll(ClaimTypes.Role)
            ?.Select(x => x.Value)
            ?.Any(x => x.Equals(role)) ?? false;
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token) {

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

        if ( !CheckValidSecurityAlgorithm(validatedToken) ) {
            throw new UnauthorizedAccessException("خطا در عدم اعتبار سنجی توکن");
        }

        return principal;
    }

    /// <summary>
    /// بررسی رمزنگاری استفاده شده در تکن
    /// </summary>
    /// <param name="validatedToken"></param>
    /// <returns></returns>
    private static bool CheckValidSecurityAlgorithm(SecurityToken validatedToken) {

        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes128KW, StringComparison.InvariantCultureIgnoreCase) &&
            jwtSecurityToken.Header.Enc.Equals(SecurityAlgorithms.Aes128CbcHmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }
}
