using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.PostgreSQL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<WashingMachine> WashingMachines { get => Set<WashingMachine>(); }

    public DbSet<DeviceGroup> DeviceGroups { get => Set<DeviceGroup>(); }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("domain");

        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.Namespace == "WashingMachineManagementApi.Persistence.PostgreSQL.Configurations"
        );
    }
}
