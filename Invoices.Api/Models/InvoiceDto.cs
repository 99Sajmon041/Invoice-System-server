using Invoices.Data.Entities;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) třída pro přenos dat o osobě přes API.
    /// Odděluje vnější datovou strukturu (např. JSON pro frontend) od vnitřní databázové entity.
    /// </summary>
    public class InvoiceDto
    {
        // Upravíme název vlastnosti v JSON výstupu (např. "_id" místo "InvoiceId") kvůli React klientovi
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public DateOnly Issued { get; set; }
        public DateOnly DueDate { get; set; }
        public string Product { get; set; } = "";
        public decimal Price { get; set; }
        public string Note { get; set; } = "";

        //vlastnosti odkazující na prodejce a kupujícího
        public int BuyerId { get; set; }
        public string BuyerName { get; set; } = null!;
        public int SellerId { get; set; }
        public string SellerName { get; set; } = null!;
    }
}
