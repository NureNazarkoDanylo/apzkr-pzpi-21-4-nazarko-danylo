using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder
            .ToTable("identity_user_tokens");

        builder
            .Property(ut => ut.UserId)
            .HasColumnName("user_id");

        builder
            .Property(ut => ut.LoginProvider)
            .HasColumnName("login_provider");

        builder
            .Property(ut => ut.Name)
            .HasColumnName("name");

        builder
            .Property(ut => ut.Value)
            .HasColumnName("value");
    }
}
