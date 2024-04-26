namespace Shop.Core.Entities
{
    public class ECategory : BasicEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<EProduct> Products { get; set; } = new HashSet<EProduct>();

    }
}
