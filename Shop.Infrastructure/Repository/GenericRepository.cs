using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repository
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BasicEntity<int>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar entidad a la base de datos", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar entidad de la base de datos", ex);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las entidades de la base de datos", ex);
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las entidades de forma asíncrona de la base de datos", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, bool>>[] include)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var item in include)
                {
                    query = query.Include(item);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las entidades de forma asíncrona con includes de la base de datos", ex);
            }
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().Where(x => x.Id == id);

                foreach (var item in include)
                {
                    query = query.Include(item);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener entidad con id {id} de forma asíncrona de la base de datos", ex);
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _context!.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener entidad con id {id} de forma asíncrona de la base de datos", ex);
            }
        }

        public async Task UpdateAsync(int id, T entity)
        {
            try
            {
                var entityValue = await GetByIdAsync(id);

                if (entityValue != null)
                {
                    _context.Update(entityValue);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar entidad con id {id} de forma asíncrona en la base de datos", ex);
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.Set<T>().CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar entidades de forma asíncrona en la base de datos", ex);
            }
        }
    }
}
