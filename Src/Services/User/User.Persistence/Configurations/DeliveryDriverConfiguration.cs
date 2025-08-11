using User.Persistence.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="DeliveryDriver"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class DeliveryDriverConfiguration : IEntityTypeConfiguration<DeliveryDriver> //SoftDeleteBaseEntityConfiguration<DeliveryDriver>
{
    //public override void Configure(EntityTypeBuilder<DeliveryDriver> builder) {

    //    base.Configure(builder);

    //    builder.ToTable("DeliveryDrivers");



    //    //اگر OnDelete رو ست نکنیم EFCore براساس optional بودن یا نبودن کلید خارجی تصمیم می گیرد که از کدام یک از حالت ها استفاده کند
    //    //اگر relation اجباری بود از نوع Cascade
    //    //اگر relation اجباری بود از نوع اختیاری بود یعنی کلید خارجی nullable بود از نوع ClientSetNull 

    //    //Cascade با حذف Parent تمامی child ها حذف می شوند
    //    //ClientSetNull با حذف Parent تمامی Child ها کلید خاجی شان توسط EFCore Null ست می شود
    //}

    public void Configure(EntityTypeBuilder<DeliveryDriver> builder) {
        builder.ToTable("DeliveryDrivers");

        builder.OwnsOne(x => x.Location);
    }
}
