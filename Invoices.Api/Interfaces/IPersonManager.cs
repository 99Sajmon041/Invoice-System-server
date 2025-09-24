using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Služba pro správu osob v aplikační vrstvě.
    /// Zajišťuje práci s DTO modely a operacemi nad osobami.
    /// </summary>
    public interface IPersonManager
    {
        /// <summary>
        /// Načte všechny osoby.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonDto"/>.</returns>
        IEnumerable<PersonDto> GetAllPersons();

        /// <summary>
        /// Vytvoří novou osobu.
        /// </summary>
        /// <param name="dto">Data osoby.</param>
        /// <returns>Vytvořená osoba jako <see cref="PersonDto"/>.</returns>
        PersonDto AddPerson(PersonDto dto);

        /// <summary>
        /// Odstraní osobu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><c>true</c>, pokud byla osoba odstraněna; jinak <c>false</c>.</returns>
        bool DeletePerson(int id);

        /// <summary>
        /// Načte osobu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><see cref="PersonDto"/> nebo <c>null</c>, pokud neexistuje.</returns>
        PersonDto? GetPersonById(int id);

        /// <summary>
        /// Aktualizuje existující osobu.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <param name="dto">Aktualizovaná data osoby.</param>
        /// <returns>Aktualizovaná osoba jako <see cref="PersonDto"/> nebo <c>null</c>, pokud nebyla nalezena.</returns>
        PersonDto? UpdatePerson(int id, PersonDto dto);

        /// <summary>
        /// Vrátí statistiky osob.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonStatisticsDto"/>.</returns>
        IEnumerable<PersonStatisticsDto> GetPersonStatistics();
    }
}
