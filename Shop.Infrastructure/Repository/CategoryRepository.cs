using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repository
{
    public class CategoryRepository : GenericRepository<ECategory>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
