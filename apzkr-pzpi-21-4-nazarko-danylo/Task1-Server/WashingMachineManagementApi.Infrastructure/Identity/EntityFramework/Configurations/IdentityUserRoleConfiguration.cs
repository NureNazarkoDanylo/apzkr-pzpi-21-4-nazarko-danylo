using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder
            .ToTable("identity_user_roles");

        builder
            .Property(ur => ur.UserId)
            .HasColumnName("user_id");

        builder
            .Property(ur => ur.RoleId)
            .HasColumnName("role_id");
    }
}
