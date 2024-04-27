using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Dto
{
    public class BaseProduct
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Name { get; set; }

        [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(1, 9999, ErrorMessage = "El precio debe estar entre {1} y {2}")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe ser un número válido")]
        public decimal Price { get; set; }
    }

    public class ProductDto : BaseProduct
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductPicture { get; set; }
    }

    public class ReturnProductDto
    {
        public int TotalItems { get; set; }
        public List<ProductDto>? ProductDtos { get; set; }
    }

    public class CreateProductDto : BaseProduct
    {
        [Required(ErrorMessage = "El ID de categoría es obligatorio")]
        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }
    }

    public class UpdateProductDto : BaseProduct
    {
        [Required(ErrorMessage = "El ID de categoría es obligatorio")]
        public int CategoryId { get; set; }

        public string? OldImage { get; set; }
        public IFormFile? Image { get; set; }
    }
}
