namespace Shop.Core.Entities.Orders
{
    public class OrderItem : BasicEntity<int>
    {
        public OrderItem() { }
        public OrderItem(ProductItemOrderd productItemOrderd, decimal price, int quantity)
        {
            ProductItemOrderd = productItemOrderd;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrderd? ProductItemOrderd { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public int Quantity { get; set; }
    }
}