namespace Shop.Core.Dto
{
    public class OrderDto
    {
        public string CartId { get; set; } = string.Empty;
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingToAddress { get; set; }
    }
}
