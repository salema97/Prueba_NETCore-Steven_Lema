﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities.Orders;

namespace Shop.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(x => x.ProductItemOrderd, n => { n.WithOwner(); });
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
