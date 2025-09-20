using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class PersonRefDto
    {
        [JsonPropertyName("_id")]
        public int PersonId { get; set; }
    }
    public class InvoiceCreateUpdateDto
    {
        public int InvoiceNumber { get; set; }
        public DateTime Issued { get; set; }
        public DateTime DueDate { get; set; }
        public string Product { get; set; } = "";
        public decimal Price { get; set; }
        public string Note { get; set; } = "";
        public PersonRefDto Seller { get; set; } = null!;
        public PersonRefDto Buyer { get; set; } = null!;
    }
}
