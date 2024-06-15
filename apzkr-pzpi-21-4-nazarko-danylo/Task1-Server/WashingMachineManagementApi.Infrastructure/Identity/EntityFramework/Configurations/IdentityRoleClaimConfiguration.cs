using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        builder
            .ToTable("identity_role_claims");

        builder
            .Property(rc => rc.Id)
            .HasColumnName("id");

        builder
            .Property(rc => rc.RoleId)
            .HasColumnName("role_id");

        builder
            .Property(rc => rc.ClaimType)
            .HasColumnName("claim_type");

        builder
            .Property(rc => rc.ClaimValue)
            .HasColumnName("claim_value");
    }
}
