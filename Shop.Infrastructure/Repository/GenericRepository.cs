using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repository
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BasicEntity<int>
    {
        public ApplicationDbContext Context { get; set; } = context;

        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Entity = await Context.Set<T>().FindAsync(id);

            _ = Context.Set<T>().Remove(Entity);
            await Context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().AsNoTracking().ToList();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, bool>>[] include)
        {
            IQueryable<T> query = Context.Set<T>();

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = Context.Set<T>();

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            return await ((DbSet<T>)query).FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var entityValue = await Context.Set<T>().FindAsync(id);

            if (entityValue != null)
            {
                Context.Update(entityValue);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }
    }
}
