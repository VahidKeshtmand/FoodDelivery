using User.Application.Common.Exceptions;
using User.Application.Common.Models;
using User.Application.Interfaces;
using User.Application.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace User.Infrastructure.Services;

/// <summary>
/// سرویس بدست آوردن اطلاعات کاربر لاگین شده
/// </summary>
internal sealed class IdentityService : IIdentityService
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

    public TokenDataModel GenerateTokenAsync(TokenUserModel user, string refreshToken) {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var encryptionSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.EncryptKey));
        var encryptingCredentials = new EncryptingCredentials(encryptionSecurityKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = new List<Claim> {
            new (ClaimTypes.NameIdentifier, user.Email.ToString()),
            new (ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new (ClaimTypes.Email, user.Email ?? "")
        };

        foreach ( var role in user.Roles ) {
            claims.Add(new(ClaimTypes.Role, role ?? ""));
        }

        var descriptor = new SecurityTokenDescriptor {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(_jwtOptions.NotBeforeMinutes),
            Expires = DateTime.Now.AddMinutes(_jwtOptions.ExpirationMinutes),
            SigningCredentials = credentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateEncodedJwt(descriptor);

        return new TokenDataModel { 
            Toke = securityToken,
            TokenType = _jwtOptions.TokenType,
            ExpireTime = descriptor.Expires.Value,
            RefreshToken = refreshToken
        };
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
            throw new CommandFailedException("خطا در عدم اعتبار سنجی توکن");
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
