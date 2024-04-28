namespace Shop.Core.Entities
{
    public class Product : BasicEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string Picture { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
