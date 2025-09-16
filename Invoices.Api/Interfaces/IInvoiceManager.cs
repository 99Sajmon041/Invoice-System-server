using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Definuje smlouvu pro správu faktur (Invoice).
    /// Rozhraní zajišťuje metody pro čtení, vytváření a mazání osob.
    /// Implementace obsahuje samotnou byznys logiku.
    /// </summary>
    public interface IInvoiceManager
    {
        /// <summary>
        /// Vrátí seznam všech faktur uložených v systému.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonDto"/> reprezentující všechny osoby.</returns>
        IEnumerable<InvoiceDto> GetAllInvoices();

        /// <summary>
        /// Vytvoří novou fakturu na základě dodaného datového objektu.
        /// </summary>
        /// <param name="dto">Objekt <see cref="PersonDto"/> s daty nové faktury.</param>
        /// <returns>Nově vytvořená faktura ve formě <see cref="InvoiceDto"/>.</returns>
        InvoiceDto AddInvoice(InvoiceDto dto);

        /// <summary>
        /// Smaže fakturu podle jejího ID.
        /// </summary>
        /// <param name="id">Jedinečné ID faktury, která má být odstraněna.</param>
        /// <returns>
        /// <c>true</c>, pokud byla faktura úspěšně smazána, 
        /// nebo <c>false</c>, pokud faktura s daným ID neexistuje.
        /// </returns>
        bool DeleteInvoice(int id);
        /// <summary>
        /// Vrátí fakturu na základě unikátního ID v parametru
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vrátí InvoiceDto objekt, pokud byla nalezena</returns>
        InvoiceDto? GetPersonById(int id);
    }
}
