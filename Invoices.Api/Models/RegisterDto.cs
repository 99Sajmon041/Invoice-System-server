using System.ComponentModel.DataAnnotations;

namespace Invoices.Api.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Jméno musí mít {2} - {1} znaků.", MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(100, ErrorMessage = "Příjmení musí mít {2} - {1} znaků.", MinimumLength = 2)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress(ErrorMessage = "Zadejte e-mail v platném formátu.")]
        [MaxLength(256, ErrorMessage = "Maximální délka e-mailu je {1} znaků.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Heslo je povinné.")]
        [MinLength(8, ErrorMessage = "Minimální délka hesla je {1} znaků.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
