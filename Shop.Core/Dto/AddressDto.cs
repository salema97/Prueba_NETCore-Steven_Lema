using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Dto
{
    public class AddressDto
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Street { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string State { get; set; }

        public string PostalCode { get; set; } = string.Empty;
    }
}
