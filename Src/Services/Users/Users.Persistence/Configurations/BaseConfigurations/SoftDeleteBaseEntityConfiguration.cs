using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Common;

namespace Users.Persistence.Configurations.BaseConfigurations;

/// <summary>
/// Provides entity configuration for entities supporting soft deletion,
/// extending <see cref="BaseEntityConfiguration{T, TKey}"/> to add soft delete properties and a global query filter.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
internal class SoftDeleteBaseEntityConfiguration<T, TKey> : BaseEntityConfiguration<T, TKey>, IEntityTypeConfiguration<T>
    where T : class, ISoftDeleteBaseEntity<TKey>
{
    public override void Configure(EntityTypeBuilder<T> builder) {

        base.Configure(builder);

        builder.Property(x => x.Deleted)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .IsRequired(false);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

internal class SoftDeleteBaseEntityConfiguration<T> : SoftDeleteBaseEntityConfiguration<T, int>, IEntityTypeConfiguration<T>
    where T : class, ISoftDeleteBaseEntity { }
