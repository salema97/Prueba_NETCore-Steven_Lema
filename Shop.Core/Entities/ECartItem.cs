namespace Shop.Core.Entities
{
    public class ECartItem : BasicEntity<int>
    {
        public string ProductName { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
