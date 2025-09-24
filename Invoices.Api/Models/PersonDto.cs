using Invoices.Data.Entities.Enums;
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
        public string Name { get; set; } = "";

        /// <summary>
        /// Identifikační číslo (např. IČ).
        /// </summary>
        public string IdentificationNumber { get; set; } = "";

        /// <summary>
        /// Daňové identifikační číslo (např. DIČ).
        /// </summary>
        public string TaxNumber { get; set; } = "";

        /// <summary>
        /// Číslo bankovního účtu.
        /// </summary>
        public string AccountNumber { get; set; } = "";

        /// <summary>
        /// Bankovní kód.
        /// </summary>
        public string BankCode { get; set; } = "";

        /// <summary>
        /// Mezinárodní číslo bankovního účtu (IBAN).
        /// </summary>
        public string Iban { get; set; } = "";

        /// <summary>
        /// Telefonní číslo.
        /// </summary>
        public string Telephone { get; set; } = "";

        /// <summary>
        /// E-mailová adresa.
        /// </summary>
        public string Mail { get; set; } = "";

        /// <summary>
        /// Ulice a číslo popisné.
        /// </summary>
        public string Street { get; set; } = "";

        /// <summary>
        /// PSČ.
        /// </summary>
        public string Zip { get; set; } = "";

        /// <summary>
        /// Město.
        /// </summary>
        public string City { get; set; } = "";

        /// <summary>
        /// Stát.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Poznámka.
        /// </summary>
        public string Note { get; set; } = "";
    }
}
