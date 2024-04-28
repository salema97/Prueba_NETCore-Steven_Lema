namespace Shop.Core.Entities
{
    public class Category : BasicEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
