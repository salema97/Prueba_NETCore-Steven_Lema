using Shop.Core.Dto;
using Shop.Core.Entities;
using Shop.Core.Sharing;

namespace Shop.Core.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ReturnProductDto> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto dto);
        new Task<bool> DeleteAsync(int id);
    }
}
