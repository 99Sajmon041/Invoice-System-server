using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový přenosový objekt referencující osobu podle identifikátoru.
    /// Používá se v DTO pro vytvoření nebo úpravu faktury.
    /// </summary>
    public class PersonRefDto
    {
        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        [JsonPropertyName("_id")]
        [Range(1, int.MaxValue)]
        public int PersonId { get; set; }
    }

    /// <summary>
    /// Datový přenosový objekt pro vytvoření nebo úpravu faktury.
    /// Obsahuje základní údaje faktury a reference na kupujícího a prodávajícího.
    /// </summary>
    public class InvoiceCreateUpdateDto : IValidatableObject
    {
        /// <summary>
        /// Číslo faktury.
        /// </summary>
        [Required(ErrorMessage = "Číslo faktury je povinné.")]
        [Range(1, int.MaxValue, ErrorMessage = "Číslo faktury musí být kladné číslo.")]
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Datum vystavení faktury.
        /// </summary>
        [Required(ErrorMessage = "Datum vystavení je povinné.")]
        [DataType(DataType.Date, ErrorMessage = "Datum vystavení není ve správném formátu.")]
        public DateTime? Issued { get; set; }

        /// <summary>
        /// Datum splatnosti faktury.
        /// </summary>
        [Required(ErrorMessage = "Splatnost je povinná.")]
        [DataType(DataType.Date, ErrorMessage = "Datum splatnosti není ve správném formátu.")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Název produktu nebo služby.
        /// </summary>
        [Required(ErrorMessage = "Produkt je povinný.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Produkt musí mít {2}-{1} znaků.")]
        public string Product { get; set; } = "";

        /// <summary>
        /// Cena faktury.
        /// </summary>
        [Required(ErrorMessage = "Cena je povinná.")]
        [Range(typeof(decimal), "1", "1000000000", ErrorMessage = "Cena musí být v rozsahu {1}–{2}.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Poznámka k faktuře.
        /// </summary>
        [Required(ErrorMessage = "Poznámka je povinná.")]
        [StringLength(200, ErrorMessage = "Poznámka může mít maximálně {1} znaků.")]
        public string Note { get; set; } = "";

        /// <summary>
        /// Prodávající osoba (referencovaná pouze identifikátorem).
        /// </summary>
        [Required(ErrorMessage = "Prodávající je povinný.")]
        public PersonRefDto Seller { get; set; } = null!;

        /// <summary>
        /// Kupující osoba (referencovaná pouze identifikátorem).
        /// </summary>
        [Required(ErrorMessage = "Kupující je povinný.")]
        public PersonRefDto Buyer { get; set; } = null!;

        /// <summary>
        /// Dodatečná doménová validace vzduhu dat
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Vráti validační error k entitě DueDate, když je menší než Issued</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> errors = new();    

            if(Issued.HasValue && DueDate.HasValue && DueDate < Issued)
            {
                errors.Add(new ValidationResult(
                    "Splatnost nesmí být před datem vystavení.",
                    new[] { nameof(DueDate) }));
            }

            return errors;
        }
    }
}
