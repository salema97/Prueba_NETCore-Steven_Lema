using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities.Orders;

namespace Shop.Infrastructure.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                new DeliveryMethod { Id = 1, ShortName = "Entrega 1", DeliveryTime = "", Description = "Descripcion Entrega 1", Price = 23 },
                new DeliveryMethod { Id = 2, ShortName = "Entrega 2", DeliveryTime = "", Description = "Descripcion Entrega 2", Price = 25 },
                new DeliveryMethod { Id = 3, ShortName = "Entrega 3", DeliveryTime = "", Description = "Descripcion Entrega 3", Price = 27 },
                new DeliveryMethod { Id = 4, ShortName = "Entrega 4", DeliveryTime = "", Description = "Descripcion Entrega 4", Price = 30 }
                );
        }
    }
}
