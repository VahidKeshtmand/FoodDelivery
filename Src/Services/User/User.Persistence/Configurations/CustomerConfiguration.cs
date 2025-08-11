using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="Customer"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>//SoftDeleteBaseEntityConfiguration<Customer> //
{
    //public override void Configure(EntityTypeBuilder<Customer> builder) {

    //    base.Configure(builder);

    //    builder.ToTable("Customers");

    //    builder.OwnsMany(x => x.Addresses)
    //        .WithOwner(y => y.Customer)
    //        .HasForeignKey(y => y.CustomerId);
    //}
    public void Configure(EntityTypeBuilder<Customer> builder) {
        builder.ToTable("Customers");

        builder.OwnsMany(x => x.Addresses, x => {
            x.Property(y => y.Street)
                .HasMaxLength(100);

            x.Property(y => y.City)
                .HasMaxLength(100);

            x.Property(y => y.LicensePlateHouse)
                .HasMaxLength(100);

            x.OwnsOne(y => y.Location);

            x.HasKey(y => y.Id);

            x.Property(y => y.Created)
                .IsRequired();

            x.Property(y => y.CreatedBy)
                .IsRequired(false);

            x.Property(y => y.LastUpdated)
                .IsRequired(false);

            x.Property(y => y.LastUpdatedBy)
                .IsRequired(false);

            x.Property(y => y.Deleted)
                .IsRequired(false);

            x.Property(y => y.DeletedBy)
                .IsRequired(false);

            x.Property(y => y.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
        });

        //builder.OwnsMany(x => x.Addresses)
        //    .WithOwner(y => y.Customer)
        //    .HasForeignKey(y => y.CustomerId);
    }
}
