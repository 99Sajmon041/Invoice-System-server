using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context) { }
    }
}
