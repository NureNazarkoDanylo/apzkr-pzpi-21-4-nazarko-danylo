using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Models;

namespace WashingMachineManagementApi.Infrastructure.Identity.EntityFramework.Configurations;

public class IdentityUserConfiguration : IEntityTypeConfiguration<ApplicationUser<string>>
{
    public void Configure(EntityTypeBuilder<ApplicationUser<string>> builder)
    {
        builder
            .ToTable("identity_users");

        // builder
        //     .Ignore(u => u.UserName);
        //
        // builder
        //     .Ignore(u => u.NormalizedUserName);

        builder
            .Ignore(u => u.PhoneNumber);

        builder
            .Ignore(u => u.PhoneNumberConfirmed);

        builder
            .Property(u => u.Id)
            .HasColumnName("id");

        builder
            .Property(u => u.Email)
            .HasColumnName("email");

        builder
            .Property(u => u.NormalizedEmail)
            .HasColumnName("normalized_email");

        builder
            .Property(u => u.EmailConfirmed)
            .HasColumnName("email_confirmed");

        builder
            .Property(u => u.PasswordHash)
            .HasColumnName("password_hash");

        builder
            .Property(u => u.SecurityStamp)
            .HasColumnName("security_stamp");

        builder
            .Property(u => u.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp");

        builder
            .Property(u => u.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled");

        builder
            .Property(u => u.LockoutEnabled)
            .HasColumnName("lockout_enabled");

        builder
            .Property(u => u.LockoutEnd)
            .HasColumnName("lockout_end");

        builder
            .Property(u => u.AccessFailedCount)
            .HasColumnName("access_failed_count");

        // builder
        //     .OwnsMany(u => u.RefreshTokens,
        //         refreshToken =>
        //         {
        //             refreshToken
        //                 .ToTable("identity_user_refresh_tokens");
        //
        //             refreshToken
        //                 .HasKey(rt => rt.Id)
        //                 .HasName("id");
        //
        //             refreshToken
        //                 .WithOwner(rt => rt.ApplicationUser)
        //                 .HasForeignKey(rt => rt.ApplicationUserId)
        //                 .HasConstraintName("fk_identityUserRefreshTokens_identityUser_userId");
        //
        //             refreshToken
        //                 .Property(rt => rt.Id)
        //                 .HasColumnName("id")
        //                 .HasColumnType("int")
        //                 .IsRequired();
        //
        //             refreshToken
        //                 .Property(rt => rt.ApplicationUserId)
        //                 .HasColumnName("identity_user_id")
        //                 .HasColumnType("int")
        //                 .IsRequired();
        //
        //             refreshToken
        //                 .Property(rt => rt.Value)
        //                 .HasColumnName("value")
        //                 .HasColumnType("varchar(256)")
        //                 .IsRequired();
        //
        //             refreshToken
        //                 .Property(rt => rt.CreationDateTimeUtc)
        //                 .HasColumnName("creation_timestamp_utc")
        //                 .HasColumnType("timestamptz")
        //                 .IsRequired();
        //
        //             refreshToken
        //                 .Property(rt => rt.ExpirationDateTimeUtc)
        //                 .HasColumnName("expiration_timestamp_utc")
        //                 .HasColumnType("timestamptz")
        //                 .IsRequired();
        //
        //             refreshToken
        //                 .Property(rt => rt.RevokationDateTimeUtc)
        //                 .HasColumnName("revokation_timestamp_utc")
        //                 .HasColumnType("timestamptz")
        //                 .IsRequired(false);
        //         }
        //     );
    }
}
