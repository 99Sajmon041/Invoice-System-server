using Invoices.Api.Models;
using Invoices.Data.Entities.Enums;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Služba pro správu faktur v aplikační vrstvě.
    /// Zajišťuje práci s DTO modely a aplikační logikou nad fakturami.
    /// </summary>
    public interface IInvoiceManager
    {
        /// <summary>
        /// Načte kolekci faktur dle zadaných filtrů.
        /// </summary>
        /// <param name="buyerId">Identifikátor kupujícího (volitelné).</param>
        /// <param name="sellerId">Identifikátor prodávajícího (volitelné).</param>
        /// <param name="product">Filtrovaný název produktu (volitelné; podmínka obsahuje).</param>
        /// <param name="minPrice">Minimální cena (volitelné).</param>
        /// <param name="maxPrice">Maximální cena (volitelné).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> odpovídající zadaným filtrům.</returns>
        IEnumerable<InvoiceDto> GetAllInvoices(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3);

        /// <summary>
        /// Vytvoří novou fakturu podle dodaného DTO.
        /// </summary>
        /// <param name="dto">Data pro vytvoření faktury.</param>
        /// <returns>Vytvořená faktura jako <see cref="InvoiceDto"/>.</returns>
        InvoiceDto AddInvoice(InvoiceCreateUpdateDto dto);

        /// <summary>
        /// Odstraní fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <returns><c>true</c>, pokud byla faktura odstraněna; jinak <c>false</c>.</returns>
        bool DeleteInvoice(int id);

        /// <summary>
        /// Načte fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <returns><see cref="InvoiceDto"/> nebo <c>null</c>, pokud neexistuje.</returns>
        InvoiceDto? GetInvoiceById(int id);

        /// <summary>
        /// Aktualizuje existující fakturu podle identifikátoru a dodaných dat.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <param name="dto">Data pro aktualizaci faktury.</param>
        /// <returns>Aktualizovaná faktura jako <see cref="InvoiceDto"/> nebo <c>null</c>, pokud nebyla nalezena.</returns>
        InvoiceDto? UpdateInvoice(int id, InvoiceCreateUpdateDto dto);

        /// <summary>
        /// Načte kolekci faktur podle identifikačního čísla subjektu a role subjektu.
        /// </summary>
        /// <param name="identificationNumber">Identifikační číslo subjektu.</param>
        /// <param name="subject">Role subjektu vůči faktuře (prodávající/kupující).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> odpovídajících zadaným podmínkám.</returns>
        IEnumerable<InvoiceDto> GetInvoicesByIdentification(string identificationNumber, Subject subject, int limit = 3);

        /// <summary>
        /// Vrátí agregované statistiky nad fakturami.
        /// </summary>
        /// <returns><see cref="InvoiceStatisticsDto"/> se souhrnnými hodnotami.</returns>
        InvoiceStatisticsDto GetInvoiceStatistics();
    }
}
