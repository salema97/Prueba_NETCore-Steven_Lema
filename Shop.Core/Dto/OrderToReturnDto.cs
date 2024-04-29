using Shop.Core.Entities.Orders;

namespace Shop.Core.Dto
{
    public class OrderToReturnDto
    {
        public int OrderId { get; set; }
        public string BuyerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; } = decimal.Zero;
        public decimal Total { get; set; } = decimal.Zero;
        public string OrderStatus { get; set; } = string.Empty;
    }
}
