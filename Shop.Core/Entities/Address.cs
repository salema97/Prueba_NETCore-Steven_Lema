namespace Shop.Core.Entities
{
    public class Address : BasicEntity<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
        public virtual AppUser AppUser { get; set; } = new AppUser();

    }
}