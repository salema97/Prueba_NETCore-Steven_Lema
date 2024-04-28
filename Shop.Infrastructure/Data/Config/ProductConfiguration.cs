using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities;

namespace Shop.Infrastructure.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(128);
            builder.Property(x => x.Description).HasMaxLength(128);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.HasData(
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 100, CategoryId = 1, Picture = "https://" },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 300, CategoryId = 1, Picture = "https://" },
                new Product { Id = 3, Name = "Product 3", Description = "Description 3", Price = 500, CategoryId = 2, Picture = "https://" },
                new Product { Id = 4, Name = "Product 4", Description = "Description 4", Price = 900, CategoryId = 3, Picture = "https://" }
                );
        }
    }
}
