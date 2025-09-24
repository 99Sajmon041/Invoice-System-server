using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový přenosový objekt faktury používaný v API vrstvách.
    /// Obsahuje základní údaje o faktuře a navázané subjekty kupujícího a prodávajícího.
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Identifikátor faktury.
        /// </summary>
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }

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
        /// Kupující subjekt.
        /// </summary>
        public PersonDto Buyer { get; set; } = null!;

        /// <summary>
        /// Prodávající subjekt.
        /// </summary>
        public PersonDto Seller { get; set; } = null!;
    }
}
