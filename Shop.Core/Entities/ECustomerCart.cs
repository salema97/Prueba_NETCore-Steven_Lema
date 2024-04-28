namespace Shop.Core.Entities
{
    public class ECustomerCart
    {
        public ECustomerCart() { }
        public ECustomerCart(string id)
        {
            Id = id;
        }

        public string? Id { get; set; }
        public List<ECartItem> CartItems { get; set; } = [];
    }
}
