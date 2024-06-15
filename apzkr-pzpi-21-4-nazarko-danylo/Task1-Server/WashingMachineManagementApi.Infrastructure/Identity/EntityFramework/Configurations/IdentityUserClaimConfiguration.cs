using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
    {
        builder
            .ToTable("identity_user_claims");

        builder
            .Property(uc => uc.Id)
            .HasColumnName("id");

        builder
            .Property(uc => uc.UserId)
            .HasColumnName("user_id");

        builder
            .Property(uc => uc.ClaimType)
            .HasColumnName("claim_type");

        builder
            .Property(uc => uc.ClaimValue)
            .HasColumnName("claim_value");
    }
}
