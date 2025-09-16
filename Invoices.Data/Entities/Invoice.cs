using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        // Vlastnosti označené jako 'required' musí být nastaveny při vytváření instance (od .NET 7)
        public required int InvoiceNumber { get; set; }
        public required DateOnly Issued { get; set; }
        public required DateOnly DueDate { get; set; }
        public required string Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }
        public required string Note { get; set; }

        //navigační entity pro Osoby (prodejce, kupující)
        public int BuyerId { get; set; }
        public required Person Buyer { get; set; }

        public int SellerId { get; set; }
        public required Person Seller { get; set; }
    }
}
