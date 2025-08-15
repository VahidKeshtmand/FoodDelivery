using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Users.Application.Interfaces;
using Users.Domain.Entities;

namespace Users.Persistence.SeedDatas;

internal sealed class RunRolesSeedData : IRunSeedData
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IRepository<Role> _roleRepository;
    private readonly ILogger _logger;

    public RunRolesSeedData(RoleManager<Role> roleManager, ILogger<RunRolesSeedData> logger, IRepository<Role> roleRepository) {
        _roleManager = roleManager;
        _logger = logger;
        _roleRepository = roleRepository;
    }

    public async Task RunAsync() {

        //var roles = typeof(AppRoles).GetFields(BindingFlags.Public | BindingFlags.Static);

        //var rolesName = roles.Select(x => x.GetValue(null)?.ToString() ?? "").ToList();
        //var rolesData = await _roleManager.Roles.ToListAsync();

        //var notExistsRoles = rolesData.Where(x => rolesName.All(y => y != x.Name));

        //foreach ( var role in notExistsRoles ) {
        //    role.Delete(0);
        //}

        //await _roleRepository.SaveChangesAsync();

        //foreach ( var role in roles ) {

        //    var roleName = role.GetValue(null)?.ToString() ?? "";

        //    if ( !await _roleManager.RoleExistsAsync(roleName) ) {

        //        var roleEntity = new Role {
        //            Name = roleName
        //        };
        //        roleEntity.Create(0);

        //        var result = await _roleManager.CreateAsync(roleEntity);
        //        if ( !result.Succeeded ) {
        //            _logger.LogError($"Add Role Failed.\n{string.Join("\n", result.Errors.Select(x => x.Description))}");
        //        }
        //    }
        //}


    }
}
