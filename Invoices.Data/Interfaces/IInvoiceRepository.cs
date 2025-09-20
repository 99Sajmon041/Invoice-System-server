using Invoices.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Invoice? GetInvoiceWithDetails(int invoiceId);
        IEnumerable<Invoice> GetAllInvoicesWithDetails(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3);
    }
}
