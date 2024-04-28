using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repository
{
    public class CategoryRepository(ApplicationDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
    }
}
