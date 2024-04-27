using Shop.Core.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Interface
{
    public interface IGenericRepository<T> where T : BasicEntity<int>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, bool>>[] include);
        IEnumerable<T> GetAll();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
