using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities.Orders;
using Shop.Core.Interface;
using Shop.Core.Services;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repository
{
    public class OrderService(IUnitOfWork unitOfWork, ApplicationDbContext context) : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ApplicationDbContext _context = context;

        public async Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, ShippingAddress shippingAddress)
        {
            var cart = await _unitOfWork.CartRepository.GetCartAsync(cartId);
            var items = new List<OrderItem>();

            foreach (var item in cart.CartItems)
            {
                var productItem = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                var productItemOrderd = new ProductItemOrderd(productItem.Id, productItem.Name, productItem.Picture);
                var orderItem = new OrderItem(productItemOrderd, item.Price, item.Quantity);

                items.Add(orderItem);
            }

            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            var deliveryMethod = await _context.DeliveryMethods.Where(x => x.Id == deliveryMethodId).FirstOrDefaultAsync();
            var subtotal = items.Sum(x => x.Price * x.Quantity);
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal, null);

            if (order == null) return null;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            await _unitOfWork.CartRepository.DeleteCartAsync(cartId);

            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod?>> GetDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _context.Orders
                .Where(x => x.Id == id && x.BuyerEmail == buyerEmail)
                .Include(x => x.OrderItems).ThenInclude(x => x.ProductItemOrderd)
                .Include(x => x.DeliveryMethod).FirstOrDefaultAsync();
            return order;
        }

        public async Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string buyerEmail)
        {
            var order = await _context.Orders
                .Where(x => x.BuyerEmail == buyerEmail)
                .Include(x => x.OrderItems).ThenInclude(x => x.ProductItemOrderd)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();

            return order;
        }
    }
}
