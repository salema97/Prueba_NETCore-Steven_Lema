using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Longitud mínima 5 caracteres")]
        public required string DisplayName { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Se espera al menos 1 letra minúscula, 1 letra mayúscula, 1 dígito, 1 carácter especial y la longitud debe estar entre 6-10 caracteres.")]
        public required string Password { get; set; }
    }
}
