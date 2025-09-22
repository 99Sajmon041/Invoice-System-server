using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    public interface IInvoiceManager
    {
        IEnumerable<InvoiceDto> GetAllInvoices(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3);
        InvoiceDto AddInvoice(InvoiceCreateUpdateDto dto);
        bool DeleteInvoice(int id);
        InvoiceDto? GetInvoiceById(int id);
        InvoiceDto? UpdateInvoice(int id, InvoiceCreateUpdateDto dto);
        IEnumerable<InvoiceDto> GetInvoicesByIdentification(string identificationNumber, bool isBuyer, int limit = 3);
        InvoiceStatisticsDto GetInvoiceStatistics();
    }
}
