using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Repozitář pro práci s entitou <see cref="Invoice"/>.
    /// Poskytuje metody pro načítání faktur včetně návazných entit a filtrování.
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        /// <summary>
        /// Inicializuje novou instanci <see cref="InvoiceRepository"/> s daným databázovým kontextem.
        /// </summary>
        /// <param name="context">Databázový kontext aplikace.</param>
        public InvoiceRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Vrátí kolekci faktur s načtenými detaily (kupující, prodávající) podle zadaných filtrů.
        /// </summary>
        /// <param name="buyerId">Identifikátor kupujícího (volitelné).</param>
        /// <param name="sellerId">Identifikátor prodávajícího (volitelné).</param>
        /// <param name="product">Filtrovaný název produktu (volitelné, obsahuje).</param>
        /// <param name="minPrice">Minimální cena (volitelné).</param>
        /// <param name="maxPrice">Maximální cena (volitelné).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce entit <see cref="Invoice"/> splňujících zadané podmínky.</returns>
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

        /// <summary>
        /// Vrátí jednu fakturu včetně navázaných entit podle identifikátoru.
        /// </summary>
        /// <param name="invoiceId">Identifikátor faktury.</param>
        /// <returns>Entita <see cref="Invoice"/> nebo <c>null</c>, pokud neexistuje.</returns>
        public Invoice? GetInvoiceWithDetails(int invoiceId)
        {
            return dbSet
                .AsNoTracking()
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .SingleOrDefault(x => x.InvoiceId == invoiceId);
        }

        /// <summary>
        /// Vrátí kolekci faktur podle role subjektu a jeho identifikačního čísla.
        /// </summary>
        /// <param name="subject">Subjekt, podle kterého se filtruje (prodávající nebo kupující).</param>
        /// <param name="identificationNumber">Identifikační číslo subjektu.</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce entit <see cref="Invoice"/> splňujících podmínky, seřazená podle data vystavení sestupně.</returns>
        public IEnumerable<Invoice> GetInvoicesBySubject(Subject subject, string identificationNumber, int limit = 3)
        {
            if (subject == Subject.Seller)
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
            else if (subject == Subject.Buyer)
            {
                return dbSet
                    .AsNoTracking()
                    .Include(x => x.Seller)
                    .Include(x => x.Buyer)
                    .Where(x => x.Buyer.IdentificationNumber == identificationNumber)
                    .OrderByDescending(x => x.Issued)
                    .Take(limit)
                    .ToList();
            }
            return Enumerable.Empty<Invoice>();
        }

        /// <summary>
        /// Vrátí dotaz nad všemi fakturami v režimu bez sledování změn.
        /// </summary>
        /// <returns><see cref="IQueryable{T}"/> pro entitu <see cref="Invoice"/> vhodné pro další kompozici dotazů.</returns>
        public IQueryable<Invoice> GetAllInvoices()
        {
            return dbSet.AsNoTracking();
        }
    }
}
