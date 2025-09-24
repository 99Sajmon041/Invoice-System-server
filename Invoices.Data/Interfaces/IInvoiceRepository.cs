using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Rozhraní repozitáře pro entitu <see cref="Invoice"/> rozšiřující základní CRUD operace.
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        /// <summary>
        /// Vrátí jednu fakturu včetně navázaných entit podle identifikátoru.
        /// </summary>
        /// <param name="invoiceId">Identifikátor faktury.</param>
        /// <returns>Entita <see cref="Invoice"/> nebo <c>null</c>, pokud neexistuje.</returns>
        Invoice? GetInvoiceWithDetails(int invoiceId);

        /// <summary>
        /// Vrátí kolekci faktur s načtenými detaily podle zadaných filtrů.
        /// </summary>
        /// <param name="buyerId">Identifikátor kupujícího (volitelné).</param>
        /// <param name="sellerId">Identifikátor prodávajícího (volitelné).</param>
        /// <param name="product">Filtrovaný název produktu (volitelné; podmínka obsahuje).</param>
        /// <param name="minPrice">Minimální cena (volitelné).</param>
        /// <param name="maxPrice">Maximální cena (volitelné).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce entit <see cref="Invoice"/> splňujících zadané podmínky.</returns>
        IEnumerable<Invoice> GetAllInvoicesWithDetails(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3);

        /// <summary>
        /// Vrátí kolekci faktur podle subjektu (prodávající/kupující) a jeho identifikačního čísla.
        /// </summary>
        /// <param name="subject">Subjekt, podle kterého se filtruje.</param>
        /// <param name="identificationNumber">Identifikační číslo subjektu.</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce entit <see cref="Invoice"/> seřazená podle data vystavení sestupně.</returns>
        IEnumerable<Invoice> GetInvoicesBySubject(Subject subject, string identificationNumber, int limit = 3);

        /// <summary>
        /// Vrátí dotaz nad všemi fakturami v režimu bez sledování změn.
        /// </summary>
        /// <returns><see cref="IQueryable{T}"/> pro entitu <see cref="Invoice"/>.</returns>
        IQueryable<Invoice> GetAllInvoices();
    }
}
