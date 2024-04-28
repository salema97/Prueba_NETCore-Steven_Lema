using Shop.Core.Entities;

namespace Shop.Core.Interface
{
    public interface ICartRepository
    {
        Task<CustomerCart?> GetCartAsync(string cartId);
        Task<CustomerCart?> UpdateCartAsync(CustomerCart customerCart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}
