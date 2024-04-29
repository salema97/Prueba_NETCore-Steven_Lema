using Shop.Core.Entities.Orders;

namespace Shop.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, ShippingAddress shippingAddress);
        Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod?>> GetDeliveryMethodsAsync();
    }
}
