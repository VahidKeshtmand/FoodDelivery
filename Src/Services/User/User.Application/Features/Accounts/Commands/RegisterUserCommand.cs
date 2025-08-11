using User.Application.Common.Models.BaseDtos;
using User.Domain.Entities;
using MediatR;

namespace User.Application.Features.Accounts.Commands;

///// <summary>
///// ثبت نام کاربر
///// </summary>
//public sealed record RegisterUserCommand : BaseCommandDto<RegisterUserCommand, UserAccount>, IRequest<int>
//{
//    /// <summary>
//    /// نام
//    /// </summary>
//    public string FirstName { get; init; }

//    /// <summary>
//    /// نام خانوادگی
//    /// </summary>
//    public string LastName { get; init; }

//    /// <summary>
//    /// کد ملی
//    /// </summary>
//    public string NationalId { get; init; }

//    /// <summary>
//    /// ایمیل
//    /// </summary>
//    public string Email { get; init; }

//    /// <summary>
//    /// شماره موبایل
//    /// </summary>
//    public string PhoneNumber { get; init; }

//    /// <summary>
//    /// پسورد
//    /// </summary>
//    public string Password { get; init; }

//    /// <summary>
//    /// تکرار پسورد
//    /// </summary>
//    public string RePassword { get; init; }
//}

//internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
//{
//    private readonly UserManager<User> _userManager;
//    private readonly RoleManager<Role> _roleManager;
//    private readonly IMapper _mapper;
//    private readonly IIdentityService _identityService;
//    private readonly IRepository<User> _userRepository;
    
//    public RegisterUserCommandHandler(
//        IMapper mapper,
//        UserManager<User> userManager,
//        IIdentityService identityService,
//        RoleManager<Role> roleManager,
//        IRepository<User> userRepository) {

//        _mapper = mapper;
//        _userManager = userManager;
//        _identityService = identityService;
//        _roleManager = roleManager;
//        _userRepository = userRepository;
//    }

//    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {

//        var entity = request.ToEntity(_mapper);
//        entity.UserName = request.Email;
//        var userId = 0;
//        if ( _identityService.IsRoleExists(AppRoles.Admin) ) {
//            userId = _identityService.GetUserId();
//        }

//        entity.Create(userId);

//        var result = await _userManager.CreateAsync(entity, request.Password);

//        if ( !result.Succeeded ) {
//            throw new CommandFailedException(result.Errors.ToDictionary(error => error.Code, error => error.Description));
//        }

//        await SetDefaultRolesForUserAsync(entity, cancellationToken);

//        return entity.Id;
//    }

//    private async Task SetDefaultRolesForUserAsync(User entity, CancellationToken cancellationToken) {
//        var role = await _roleManager.FindByNameAsync(AppRoles.User);
//        entity.AddRole(new UserRole { RoleId = role!.Id, UserId = entity.Id });
//        await _userRepository.SaveChangesAsync(cancellationToken);
//    }
//}
