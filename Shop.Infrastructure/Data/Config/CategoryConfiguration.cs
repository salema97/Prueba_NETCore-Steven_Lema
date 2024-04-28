using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities;

namespace Shop.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);

            builder.HasData(
                new Category { Id = 1, Name = "Categoria 1", Description = "Descripcion Categoria 1" },
                new Category { Id = 2, Name = "Categoria 2", Description = "Descripcion Categoria 2" },
                new Category { Id = 3, Name = "Categoria 3", Description = "Descripcion Categoria 3" }
                );
        }
    }
}
