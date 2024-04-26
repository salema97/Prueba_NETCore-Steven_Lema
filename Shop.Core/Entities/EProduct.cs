namespace Shop.Core.Entities
{
    public class EProduct : BasicEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Picture { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public virtual ECategory? Category { get; set; }
    }
}
