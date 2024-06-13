using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Configurations;

public class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase<string>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();
    }
}
