using Shop.Core.Entities;

namespace Shop.Core.Dto
{
    public class OrderItemDto : BasicEntity<int>
    {
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public int Quantity { get; set; }
    }
}