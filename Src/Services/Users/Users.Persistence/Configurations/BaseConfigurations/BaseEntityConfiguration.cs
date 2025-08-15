using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Common;

namespace Users.Persistence.Configurations.BaseConfigurations;

/// <summary>
/// Provides base entity configuration for entities implementing <see cref="IBaseEntity{TKey}"/>.
/// Configures primary key and auditing properties such as creation and last update timestamps and user IDs.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
internal class BaseEntityConfiguration<T, TKey> : IEntityTypeConfiguration<T> 
    where T : class, IBaseEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder) {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired(false);


        builder.Property(x => x.LastUpdated)
            .IsRequired(false);

        builder.Property(x => x.LastUpdatedBy)
            .IsRequired(false);
    }
}

internal class BaseEntityConfiguration<T> : BaseEntityConfiguration<T, int> , IEntityTypeConfiguration<T>
    where T : class, IBaseEntity { }
