using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Dto
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string ProductName { get; set; }

        [Required]
        public required string Picture { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public int Quantity { get; set; }

        [Required]
        public required string Category { get; set; }
    }
}
