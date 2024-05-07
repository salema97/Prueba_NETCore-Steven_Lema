namespace Shop.Core.Entities
{
    public class Address : BasicEntity<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }

    }
}