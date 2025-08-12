using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="Customer"/> entity.
/// </summary>
internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder) {
        builder.ToTable("Customers");

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(100);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.SecurityStamp)
            .HasMaxLength(2000);

        builder.Property(x => x.ConcurrencyStamp)
            .HasMaxLength(2000);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired();

        builder.HasMany(e => e.UserRoles)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.HasQueryFilter(x =>
            x.Addresses.Any(y => !y.IsDeleted));

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
    }
}
