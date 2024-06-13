using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WashingMachineManagementApi.Application.Common.Models;
using WashingMachineManagementApi.Infrastructure.Identity.Common.Models;
using WashingMachineManagementApi.Infrastructure.Identity.MongoDb.Models;

namespace WashingMachineManagementApi.Infrastructure.Identity;

public static class IdentitySeeder
{
    private static UserManager<ApplicationUser<string>> _userManager;
    private static RoleManager<ApplicationRole<string>> _roleManager;

    public static void SeedIdentity(IServiceScope serviceScope)
    {
        _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser<string>>>();
        _userManager.UserValidators.Clear();
        _userManager.PasswordValidators.Clear();

        _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole<string>>>();
        _roleManager.RoleValidators.Clear();

        SeedRoles();
        SeedUsers();
    }

    private static void SeedRoles()
    {
        var roles = Enum.GetValues(typeof(IdentityRoles)).Cast<IdentityRoles>();

        foreach (var role in roles)
        {
            var roleString = role.ToString();

            var roleExists = _roleManager.RoleExistsAsync(roleString).Result;

            if (roleExists)
            {
                continue;
            }

            _roleManager.CreateAsync(new ApplicationRole<string>() 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = roleString,
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                });
        }
    }

    private static void SeedUsers()
    {
        var user = new ApplicationUser<string>
        {
            Id = Guid.NewGuid().ToString(),
            Email = "user",
            NormalizedEmail = "user",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Roles = _roleManager.Roles.Where(r => r.Name == IdentityRoles.User.ToString()).Select(r => r.Id).ToList(),
            RefreshTokens = new RefreshToken<string>[0] 
        };

        var userExists = _userManager.FindByEmailAsync(user.Email).Result is not null;
        if (!userExists)
        {
            var hashed = _userManager.PasswordHasher.HashPassword(user, "user");
            user.PasswordHash = hashed;
            _userManager.CreateAsync(user);
        }



        var admin = new ApplicationUser<string>
        {
            Id = Guid.NewGuid().ToString(),
            Email = "admin",
            NormalizedEmail = "admin",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Roles = _roleManager.Roles.Where(r => r.Name == IdentityRoles.Administrator.ToString()).Select(r => r.Id).ToList(),
            RefreshTokens = new RefreshToken<string>[0] 
        };

        userExists = _userManager.FindByEmailAsync(admin.Email).Result is not null;
        if (!userExists)
        {
            var hashed = _userManager.PasswordHasher.HashPassword(admin, "admin");
            admin.PasswordHash = hashed;
            _userManager.CreateAsync(admin);
            _userManager.AddToRoleAsync(admin, IdentityRoles.Administrator.ToString());
        }



        var adminUser = new ApplicationUser<string>
        {
            Id = Guid.NewGuid().ToString(),
            Email = "adminUser",
            NormalizedEmail = "ADMINUSER",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Roles = _roleManager.Roles.Where(r => r.Name == IdentityRoles.Administrator.ToString() || r.Name == IdentityRoles.User.ToString()).Select(r => r.Id).ToList(),
            RefreshTokens = new RefreshToken<string>[0] 
        };

        userExists = _userManager.FindByEmailAsync(adminUser.Email).Result is not null;
        if (!userExists)
        {
            var hashed = _userManager.PasswordHasher.HashPassword(adminUser, "adminUser");
            adminUser.PasswordHash = hashed;
            _userManager.CreateAsync(adminUser);
        }
    }
}
