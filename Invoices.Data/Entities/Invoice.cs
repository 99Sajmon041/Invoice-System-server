using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Entities
{
    /// <summary>
    /// Reprezentuje fakturu v systému, včetně základních údajů, částky a vazeb na kupujícího a prodávajícího.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Primární klíč faktury.
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Číslo faktury.
        /// </summary>
        public required int InvoiceNumber { get; set; }

        /// <summary>
        /// Datum vystavení faktury.
        /// </summary>
        public required DateTime Issued { get; set; }

        /// <summary>
        /// Datum splatnosti faktury.
        /// </summary>
        public required DateTime DueDate { get; set; }

        /// <summary>
        /// Popis produktu nebo služby účtované na faktuře.
        /// </summary>
        public required string Product { get; set; }

        /// <summary>
        /// Cena faktury v měně systému.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }

        /// <summary>
        /// Poznámka k faktuře.
        /// </summary>
        public required string Note { get; set; }

        /// <summary>
        /// Identifikátor kupujícího (cizí klíč na <see cref="Person"/>).
        /// </summary>
        public int BuyerId { get; set; }

        /// <summary>
        /// Navigační vlastnost na kupujícího.
        /// </summary>
        public required Person Buyer { get; set; }

        /// <summary>
        /// Identifikátor prodávajícího (cizí klíč na <see cref="Person"/>).
        /// </summary>
        public int SellerId { get; set; }

        /// <summary>
        /// Navigační vlastnost na prodávajícího.
        /// </summary>
        public required Person Seller { get; set; }
    }
}
