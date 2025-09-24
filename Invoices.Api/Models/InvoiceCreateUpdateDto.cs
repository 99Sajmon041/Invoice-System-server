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
        public int PersonId { get; set; }
    }

    /// <summary>
    /// Datový přenosový objekt pro vytvoření nebo úpravu faktury.
    /// Obsahuje základní údaje faktury a reference na kupujícího a prodávajícího.
    /// </summary>
    public class InvoiceCreateUpdateDto
    {
        /// <summary>
        /// Číslo faktury.
        /// </summary>
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Datum vystavení faktury.
        /// </summary>
        public DateTime Issued { get; set; }

        /// <summary>
        /// Datum splatnosti faktury.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Název produktu nebo služby.
        /// </summary>
        public string Product { get; set; } = "";

        /// <summary>
        /// Cena faktury.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Poznámka k faktuře.
        /// </summary>
        public string Note { get; set; } = "";

        /// <summary>
        /// Prodávající osoba (referencovaná pouze identifikátorem).
        /// </summary>
        public PersonRefDto Seller { get; set; } = null!;

        /// <summary>
        /// Kupující osoba (referencovaná pouze identifikátorem).
        /// </summary>
        public PersonRefDto Buyer { get; set; } = null!;
    }
}
