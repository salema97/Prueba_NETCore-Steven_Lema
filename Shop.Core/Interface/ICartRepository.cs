using Shop.Core.Entities;

namespace Shop.Core.Interface
{
    public interface ICartRepository
    {
        Task<ECustomerCart?> GetCartAsync(string cartId);
        Task<ECustomerCart?> UpdateCartAsync(ECustomerCart customerCart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}
