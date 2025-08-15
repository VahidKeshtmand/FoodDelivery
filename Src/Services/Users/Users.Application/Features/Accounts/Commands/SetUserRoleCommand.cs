using MediatR;

namespace Users.Application.Features.Accounts.Commands;

/// <summary>
/// اضافه کردن نقش به نقش های کاربر
/// </summary>
public sealed record SetUserRoleCommand : IRequest
{
    /// <summary>
    /// شناسه کاربر
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// شناسه نقش ها
    /// </summary>
    public List<int> RoleIds { get; init; }
}

//internal sealed class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand>
//{
//    private readonly UserManager<User> _userManager;
//    private readonly RoleManager<Role> _roleManager;
//    private readonly IRepository<User> _userRepository;

//    public SetUserRoleCommandHandler(UserManager<User> userManager, IRepository<User> userRepository, RoleManager<Role> roleManager) {
//        _userManager = userManager;
//        _userRepository = userRepository;
//        _roleManager = roleManager;
//    }

//    public async Task Handle(SetUserRoleCommand request, CancellationToken cancellationToken) {

//        var roles = await _roleManager.Roles.Where(x => request.RoleIds.Any(y => x.Id == y))
//            .Select(x => x.Name ?? "").ToListAsync(cancellationToken: cancellationToken);

//        var user = await _userManager.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == request.UserId)
//            ?? throw new NotFoundException("کاربر", request.UserId);

//        if ( user.UserRoles != null && user.UserRoles.Any() ) {
//            user.DeleteRoles();
//            await _userRepository.SaveChangesAsync(cancellationToken);
//        }

//        var result = await _userManager.AddToRolesAsync(user, roles);

//        if ( !result.Succeeded ) {
//            throw new CommandFailedException(result.Errors.ToDictionary(error => error.Code, error => error.Description));
//        }

//    }
//}
