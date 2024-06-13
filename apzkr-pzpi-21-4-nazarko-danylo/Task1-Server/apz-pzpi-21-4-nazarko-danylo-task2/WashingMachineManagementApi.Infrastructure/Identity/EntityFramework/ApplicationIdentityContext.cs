using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Models;

namespace AutobusApi.Infrastructure.Identity;

public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser<string>, ApplicationRole<string>, string>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("identity");

        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.Namespace == "WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations"
        );
    }
}
