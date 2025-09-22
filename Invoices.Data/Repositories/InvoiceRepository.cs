using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context) { }
        public IEnumerable<Invoice> GetAllInvoicesWithDetails(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3)
        {
            IQueryable<Invoice> invoices = dbSet.AsNoTracking()
                .Include(x => x.Buyer)
                .Include(x => x.Seller);

            if (buyerId != null)
                invoices = invoices.Where(x => x.BuyerId == buyerId);

            if (sellerId != null)
                invoices = invoices.Where(x => x.SellerId == sellerId);

            if (product != null)
                invoices = invoices.Where(x => x.Product.Contains(product));

            if (minPrice != null)
                invoices = invoices.Where(x => x.Price >= minPrice);

            if (maxPrice != null)
                invoices = invoices.Where(x => x.Price <= maxPrice);

            List<Invoice> result = invoices.Take(limit).ToList();

            return result;
        }

        public Invoice? GetInvoiceWithDetails(int invoiceId)
        {
            return dbSet
                .AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .SingleOrDefault(x => x.InvoiceId == invoiceId);
        }

        public IEnumerable<Invoice> GetSalesByIdentification(string identificationNumber, int limit = 3)
        {
            return dbSet
                .AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .Where(x => x.Seller.IdentificationNumber == identificationNumber)
                .OrderByDescending(x => x.Issued)
                .Take(limit)
                .ToList();
        }

        public IEnumerable<Invoice> GetPurchasesByIdentification(string identificationNumber, int limit = 3)
        {
            return dbSet
                .AsNoTracking()
                .Include(x => x.Buyer)
                .Include(x => x.Seller)
                .Where(x => x.Buyer.IdentificationNumber == identificationNumber)
                .OrderByDescending(x => x.Issued)
                .Take(limit)
                .ToList();
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return dbSet.AsNoTracking();
        }
    }
}
