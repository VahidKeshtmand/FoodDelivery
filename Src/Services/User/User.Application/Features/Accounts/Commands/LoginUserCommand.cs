using AutoMapper;
using User.Application.Common.Exceptions;
using User.Application.Common.Models;
using User.Application.Common.Utilities;
using User.Application.Interfaces;
using User.Application.Options;
using User.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace User.Application.Features.Accounts.Commands;

/// <summary>
/// ورود کاربر
/// </summary>
public sealed record LoginUserCommand : IRequest<TokenDataModel>
{
    /// <summary>
    /// نام کاربری
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    /// رمز عبور
    /// </summary>
    public string Password { get; init; }
}

//internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenDataModel>
//{
//    private readonly UserManager<User> _userManager;
//    private readonly IMapper _mapper;
//    private readonly IIdentityService _identityService;
//    private readonly IRepository<RefreshToken> _repository;
//    private readonly JwtOptions _jwtOptions;

//    public LoginUserCommandHandler(
//        UserManager<User> userManager,
//        IMapper mapper,
//        IIdentityService identityService,
//        IRepository<RefreshToken> repository,
//        IOptions<JwtOptions> jwtOptions) {

//        _userManager = userManager;
//        _mapper = mapper;
//        _identityService = identityService;
//        _repository = repository;
//        _jwtOptions = jwtOptions.Value;
//    }

//    public async Task<TokenDataModel> Handle(LoginUserCommand request, CancellationToken cancellationToken) {

//        var user = await _userManager.FindByNameAsync(request.UserName)
//            ?? throw new NotFoundException("رمز عبور یا نام کاربری اشتباه است.");

//        if ( !await _userManager.CheckPasswordAsync(user, request.Password) ) {
//            throw new NotFoundException("رمز عبور یا نام کاربری اشتباه است.");
//        }

//        var refreshToken = await CreateRefreshTokenAsync(user, cancellationToken);

//        var userModel = await GenerateTokenUserModelAsync(user);

//        return _identityService.GenerateTokenAsync(userModel, refreshToken);
//    }

//    private async Task<TokenUserModel> GenerateTokenUserModelAsync(User user) {
//        var userModel = _mapper.Map<TokenUserModel>(user);
//        userModel.Roles = (await _userManager.GetRolesAsync(user)).ToList();
//        return userModel;
//    }

//    private async Task<string> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken) {
//        var refreshToken = Guid.NewGuid().GetHa256Hash();
//        var refreshTokenEntity = new RefreshToken {
//            Token = refreshToken,
//            UserId = user.Id,
//            ExpirationDate = DateTime.Now.AddMonths(_jwtOptions.RefreshTokenExpirationMonths),
//        };
//        refreshTokenEntity.Create(0);
//        await _repository.DbSet.AddAsync(refreshTokenEntity, cancellationToken);
//        await _repository.SaveChangesAsync(cancellationToken);
//        return refreshToken;
//    }
//}
