using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="Address"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
//internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
//{
//    public void Configure(EntityTypeBuilder<Address> builder) {
//        builder.Property(x => x.Street)
//            .HasMaxLength(100);

//        builder.Property(x => x.City)
//            .HasMaxLength(100);

//        builder.Property(x => x.LicensePlateHouse)
//            .HasMaxLength(100);

//        builder.OwnsOne(x => x.Location);

//        builder.HasKey(x => x.Id);

//        builder.Property(x => x.Created)
//            .IsRequired();

//        builder.Property(x => x.CreatedBy)
//            .IsRequired(false);

//        builder.Property(x => x.LastUpdated)
//            .IsRequired(false);

//        builder.Property(x => x.LastUpdatedBy)
//            .IsRequired(false);

//        builder.Property(x => x.Deleted)
//            .IsRequired(false);

//        builder.Property(x => x.DeletedBy)
//            .IsRequired(false);

//        builder.Property(x => x.IsDeleted)
//            .HasDefaultValue(false)
//            .IsRequired();

//        builder.HasQueryFilter(x => !x.IsDeleted);
//    }
//}
