using AutoMapper;
using User.Application.Common.Exceptions;
using User.Application.Common.Models;
using User.Application.Common.Utilities;
using User.Application.Interfaces;
using User.Application.Options;
using User.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace User.Application.Features.Accounts.Commands;

public sealed record RefreshTokenCommand : IRequest<TokenDataModel>
{
    /// <summary>
    /// توکن
    /// </summary>
    public string Token { get; init; }

    /// <summary>
    /// رفرش توکن
    /// </summary>
    public string RefreshToken { get; init; }
}

//internal sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDataModel>
//{
//    private readonly IIdentityService _identityService;
//    private readonly IRepository<RefreshToken> _repository;
//    private readonly UserManager<User> _userManager;
//    private readonly IMapper _mapper;
//    private readonly JwtOptions _jwtOptions;

//    public RefreshTokenCommandHandler(
//        IIdentityService identityService,
//        IRepository<RefreshToken> repository,
//        UserManager<User> userManager,
//        IMapper mapper,
//        IOptions<JwtOptions> jwtOptions) {

//        _identityService = identityService;
//        _repository = repository;
//        _userManager = userManager;
//        _mapper = mapper;
//        _jwtOptions = jwtOptions.Value;
//    }

//    public async Task<TokenDataModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken) {

//        await UseRefreshTokenAsync(request, cancellationToken);

//        var userId = ValidateTokenAndGetUserId(request);

//        var refreshToken = Guid.NewGuid().GetHa256Hash();

//        await StoreRefreshTokenAsync(userId, refreshToken, cancellationToken);

//        var userModel = await GenerateTokenModel(userId);

//        return _identityService.GenerateTokenAsync(userModel, refreshToken);
//    }

//    private string ValidateTokenAndGetUserId(RefreshTokenCommand request) {
//        var validatedToken = _identityService.GetPrincipalFromToken(request.Token);
//        var userId = validatedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
//            ?? throw new CommandFailedException("عدم اعتبار رفرش توکن.");
//        return userId;
//    }

//    private async Task UseRefreshTokenAsync(RefreshTokenCommand request, CancellationToken cancellationToken) {
//        var entityRefreshToken = await _repository.Query
//            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken)
//                ?? throw new NotFoundException("رفرش توکن یافت نشد.");

//        if ( DateTime.Now > entityRefreshToken.ExpirationDate || entityRefreshToken.Used ) {
//            throw new CommandFailedException("عدم اعتبار رفرش توکن.");
//        }

//        entityRefreshToken.UseToken();
//        await _repository.SaveChangesAsync(cancellationToken);
//    }

//    private async Task<TokenUserModel> GenerateTokenModel(string userId) {
//        var user = await _userManager.FindByIdAsync(userId)
//            ?? throw new NotFoundException("کاربر یافت نشد.");
//        var userModel = _mapper.Map<TokenUserModel>(user);
//        userModel.Roles = (await _userManager.GetRolesAsync(user)).ToList();
//        return userModel;
//    }

//    private async Task StoreRefreshTokenAsync(string userId, string refreshToken, CancellationToken cancellationToken) {
//        var refreshTokenEntity = new RefreshToken {
//            Token = refreshToken,
//            UserId = int.Parse(userId),
//            ExpirationDate = DateTime.Now.AddMonths(_jwtOptions.RefreshTokenExpirationMonths),
//        };
//        refreshTokenEntity.Create(0);
//        await _repository.DbSet.AddAsync(refreshTokenEntity, cancellationToken);
//        await _repository.SaveChangesAsync(cancellationToken);
//    }
//}
