using Invoices.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový přenosový objekt osoby používaný v API vrstvách.
    /// Obsahuje identifikační, kontaktní a adresní údaje.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        [JsonPropertyName("_id")]
        public int PersonId { get; set; }

        /// <summary>
        /// Název/jméno osoby.
        /// </summary>
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Jméno musí mít {2}–{1} znaků.")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Identifikační číslo (např. IČ).
        /// </summary>
        [Required(ErrorMessage = "IČO je povinné.")]
        [StringLength(8, MinimumLength = 8,ErrorMessage = "IČO musí mít přesně 8 znaků.")]
        public string IdentificationNumber { get; set; } = "";

        /// <summary>
        /// Daňové identifikační číslo (např. DIČ).
        /// </summary>
        [Required(ErrorMessage = "DIČ je povinné.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "DIČ musí mít {2}–{1} znaků.")]
        public string TaxNumber { get; set; } = "";

        /// <summary>
        /// Číslo bankovního účtu.
        /// </summary>
        [Required(ErrorMessage = "Číslo bankovního účtu je povinné.")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "Číslo účtu musí mít {2}–{1} znaků.")]
        public string AccountNumber { get; set; } = "";

        /// <summary>
        /// Bankovní kód.
        /// </summary>
        [Required(ErrorMessage = "Kód banky je povinný.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Kód banky musí mít přesně 4 znaky.")]
        public string BankCode { get; set; } = "";

        /// <summary>
        /// Mezinárodní číslo bankovního účtu (IBAN).
        /// </summary>
        [Required(ErrorMessage = "IBAN je povinný.")]
        [StringLength(24, MinimumLength = 24, ErrorMessage = "IBAN musí mít přesně 24 znaků (bez mezer).")]
        public string Iban { get; set; } = "";

        /// <summary>
        /// Telefonní číslo.
        /// </summary>
        [Required(ErrorMessage = "Telefon je povinný.")]
        [Phone(ErrorMessage = "Telefon není ve správném formátu.")]
        public string Telephone { get; set; } = "";

        /// <summary>
        /// E-mailová adresa.
        /// </summary>
        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress(ErrorMessage = "E-mail není ve správném formátu.")]
        public string Mail { get; set; } = "";

        /// <summary>
        /// Ulice a číslo popisné.
        /// </summary>
        [Required(ErrorMessage = "Ulice je povinná.")]
        [StringLength(50, ErrorMessage = "Ulice může mít maximálně {1} znaků.")]
        public string Street { get; set; } = "";

        /// <summary>
        /// PSČ.
        /// </summary>
        [Required(ErrorMessage = "PSČ je povinné.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "PSČ musí mít {2}–{1} znaků.")]
        public string Zip { get; set; } = "";

        /// <summary>
        /// Město.
        /// </summary>
        [Required(ErrorMessage = "Město je povinné.")]
        [StringLength(30, ErrorMessage = "Město může mít maximálně {1} znaků.")]
        public string City { get; set; } = "";

        /// <summary>
        /// Stát.
        /// </summary>
        [Required(ErrorMessage = "Země je povinná.")]
        public Country? Country { get; set; }

        /// <summary>
        /// Poznámka.
        /// </summary>
        [Required(ErrorMessage = "Poznámka je povinná.")]
        [StringLength(150, ErrorMessage = "Poznámka může mít maximálně {1} znaků.")]
        public string Note { get; set; } = "";
    }
}
