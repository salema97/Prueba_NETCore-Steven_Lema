using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Dto
{
    public class CustomerCartDto
    {
        [Required]
        public required string Id { get; set; }
        public List<CartItemDto> CartItems { get; set; } = [];
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; } = string.Empty;
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal ShippingPrice { get; set; }
    }
}
