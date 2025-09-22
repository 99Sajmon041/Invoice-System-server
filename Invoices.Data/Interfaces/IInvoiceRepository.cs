using Invoices.Data.Entities;

namespace Invoices.Data.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Invoice? GetInvoiceWithDetails(int invoiceId);
        IEnumerable<Invoice> GetAllInvoicesWithDetails(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3);
        IEnumerable<Invoice> GetSalesByIdentification(string identificationNumber, int limit = 3);
        IEnumerable<Invoice> GetPurchasesByIdentification(string identificationNumber, int limit = 3);
        IQueryable<Invoice> GetAllInvoices();
    }
}