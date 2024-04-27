using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using System.Reflection;

namespace Shop.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<ECategory> Categories { get; set; }
        public virtual DbSet<EProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
