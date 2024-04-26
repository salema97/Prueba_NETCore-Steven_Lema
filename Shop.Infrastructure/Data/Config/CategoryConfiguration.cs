using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities;

namespace Shop.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<ECategory>
    {
        public void Configure(EntityTypeBuilder<ECategory> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(50);

            builder.HasData(
                new ECategory { Id = 1, Name = "Categoria 1", Description = "Descripcion Categoria 1" },
                new ECategory { Id = 2, Name = "Categoria 2", Description = "Descripcion Categoria 2" },
                new ECategory { Id = 3, Name = "Categoria 3", Description = "Descripcion Categoria 3" }
                );
        }
    }
}
