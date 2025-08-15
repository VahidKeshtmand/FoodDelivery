using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;
using Users.Persistence.Configurations.BaseConfigurations;

namespace Users.Persistence.Configurations;

internal sealed class OtpCodeConfiguration : BaseEntityConfiguration<OtpCode>
{
    public override void Configure(EntityTypeBuilder<OtpCode> builder) {
        base.Configure(builder); 

        builder.ToTable("OtpCodes");

        builder.Property(x => x.PhoneNumber)
            .IsUnicode(false)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.Code)
            .IsUnicode(false)
            .HasMaxLength(6)
            .IsRequired();

    }
}
