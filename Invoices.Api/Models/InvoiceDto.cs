using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class InvoiceDto
    {
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime Issued { get; set; }
        public DateTime DueDate { get; set; }
        public string Product { get; set; } = "";
        public decimal Price { get; set; }
        public string Note { get; set; } = "";
        public PersonDto Buyer { get; set; } = null!;
        public PersonDto Seller { get; set; } = null!;
    }
}
