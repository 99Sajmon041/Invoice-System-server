using System.ComponentModel.DataAnnotations;

namespace Invoices.Api.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress(ErrorMessage = "Zadejte e-mail v platném formátu.")]
        [MaxLength(256, ErrorMessage = "Maximální délka e-mailu je {1} znaků.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
