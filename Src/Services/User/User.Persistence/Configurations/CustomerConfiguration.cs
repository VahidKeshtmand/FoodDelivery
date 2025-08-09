using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="Customer"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class CustomerConfiguration : SoftDeleteBaseEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder) {
        
        base.Configure(builder);

        builder.ToTable("Customers");

        builder.OwnsMany(x => x.Addresses)
            .WithOwner(y => y.Customer)
            .HasForeignKey(y => y.CustomerId);
    }
}
