namespace Shop.Core.Entities.Orders
{
    public class ShippingAddress
    {
        public ShippingAddress() { }
        public ShippingAddress(string firstName, string lastName, string street, string city, string state, string postalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}