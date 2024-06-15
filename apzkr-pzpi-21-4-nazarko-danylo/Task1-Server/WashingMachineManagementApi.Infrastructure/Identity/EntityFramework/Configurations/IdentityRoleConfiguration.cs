using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder
            .ToTable("identity_roles");

        builder
            .Property(r => r.Id)
            .HasColumnName("id");

        builder
            .Property(r => r.Name)
            .HasColumnName("name");

        builder
            .Property(r => r.NormalizedName)
            .HasColumnName("normalized_name");

        builder
            .Property(r => r.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");
    }
}
