using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Configurations;

public class DeviceGroupConfiguration : EntityBaseConfiguration<DeviceGroup>
{
    public override void Configure(EntityTypeBuilder<DeviceGroup> builder)
    {
        base.Configure(builder);

        builder
            .ToTable("device_groups")
            .HasKey(e => e.Id);

        // builder
        //     .Property(e => e.Amount)
        //         .HasColumnName("amount")
        //         .IsRequired();
        //
        // builder
        //     .Property(e => e.Currency)
        //         .HasColumnName("currency")
        //         .HasConversion(
        //             t => t.Name,
        //             s => Domain.Enums.Currency.FromName(s)
        //         )
        //         .IsRequired();
        //
        // builder
        //     .Property(e => e.Category)
        //         .HasColumnName("category")
        //         .HasConversion(
        //             t => t.Name,
        //             s => Domain.Enums.Category.FromName(s)
        //         )
        //         .IsRequired();
        //
        // builder
        //     .Property(e => e.Time)
        //         .HasColumnName("time")
        //         .IsRequired();
        //
        // builder
        //     .Property(e => e.Description)
        //         .HasColumnName("description")
        //         .IsRequired();
        //
        // builder
        //     .HasOne(e => e.Account)
        //     .WithMany(e => e.Expenses)
        //     .HasForeignKey(e => e.AccountId)
        //     .HasConstraintName("fk_expense_budget_id")
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // builder
        //     .Property(e => e.UserId)
        //         .HasColumnName("fk_expense_user_id")
        //         .IsRequired();
            // .OnDelete(DeleteBehavior.Cascade);
    }
}

