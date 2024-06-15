using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder
            .ToTable("identity_user_logins");

        builder
            .Property(ul => ul.LoginProvider)
            .HasColumnName("login_provider");

        builder
            .Property(ul => ul.ProviderKey)
            .HasColumnName("provider_key");

        builder
            .Property(ul => ul.ProviderDisplayName)
            .HasColumnName("provider_display_name");

        builder
            .Property(ul => ul.UserId)
            .HasColumnName("user_id");
    }
}
