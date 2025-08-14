using AutoMapper;
using User.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using User.Application.Common.Exceptions;
using User.Application.Common.Utilities;
using User.Application.Interfaces;
using User.Application.Options;
using User.Domain.Entities;

namespace User.Application.Features.Accounts.Commands;

/// <summary>
/// Represents a command to authenticate a user and generate a JWT token.
/// </summary>
public sealed record LoginUserCommand : IRequest<TokenDataModel>
{
    /// <summary>
    /// The username or email used for user authentication.
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    /// The user's password for authentication.
    /// </summary>
    public string Password { get; init; }
}

internal sealed class LoginUserCommandHandler(
    UserManager<UserAccount> userManager,
    IMapper mapper,
    IIdentityService identityService,
    IRepository<RefreshToken> repository,
    IOptions<JwtOptions> jwtOptions)
    : IRequestHandler<LoginUserCommand, TokenDataModel>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<TokenDataModel> Handle(LoginUserCommand request, CancellationToken cancellationToken) {

        var sdf = await userManager.Users.ToListAsync(cancellationToken);

        var user = await userManager.FindByNameAsync(request.UserName)
            ?? throw new NotFoundException("Username or password is incorrect.");

        if ( !await userManager.CheckPasswordAsync(user, request.Password) ) {
            throw new NotFoundException("Username or password is incorrect.");
        }

        var refreshToken = await CreateRefreshTokenAsync(user, cancellationToken);

        var userModel = await GenerateTokenUserModelAsync(user);

        return identityService.GenerateTokenAsync(userModel, refreshToken);
    }

    private async Task<TokenUserModel> GenerateTokenUserModelAsync(UserAccount user) {
        var userModel = new TokenUserModel {
            Email = user.Email ?? "",
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            Roles = (await userManager.GetRolesAsync(user)).ToList()
        };
        return userModel;
    }

    private async Task<string> CreateRefreshTokenAsync(UserAccount user, CancellationToken cancellationToken) {
        var refreshToken = Guid.NewGuid().GetHa256Hash();
        var refreshTokenEntity = new RefreshToken {
            Token = refreshToken,
            UserId = user.Id,
            ExpirationDate = DateTime.Now.AddMonths(_jwtOptions.RefreshTokenExpirationMonths),
        };
        refreshTokenEntity.Create(0);
        await repository.DbSet.AddAsync(refreshTokenEntity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return refreshToken;
    }
}
