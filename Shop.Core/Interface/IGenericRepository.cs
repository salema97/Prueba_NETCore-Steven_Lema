using Shop.Core.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Interface
{
    public interface IGenericRepository<T> where T : BasicEntity<int>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, bool>>[] include);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
