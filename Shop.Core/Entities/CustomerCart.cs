namespace Shop.Core.Entities
{
    public class CustomerCart
    {
        public CustomerCart() { }
        public CustomerCart(string id)
        {
            Id = id;
        }

        public string Id { get; set; } = string.Empty;
        public List<CartItem> CartItems { get; set; } = [];
    }
}
