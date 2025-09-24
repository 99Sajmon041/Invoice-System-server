using Invoices.Data.Entities;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Rozhraní repozitáře pro entitu <see cref="Person"/> rozšiřující základní CRUD operace.
    /// </summary>
    public interface IPersonRepository : IBaseRepository<Person>
    {
        /// <summary>
        /// Vrátí kolekci osob dle příznaku skrytí.
        /// </summary>
        /// <param name="hidden">Příznak skrytí osoby.</param>
        /// <returns>Kolekce entit <see cref="Person"/> odpovídajících filtru.</returns>
        IEnumerable<Person> GetByHidden(bool hidden);

        /// <summary>
        /// Vytvoří dotaz nad všemi osobami v režimu bez sledování změn.
        /// </summary>
        /// <returns><see cref="IQueryable{T}"/> pro entitu <see cref="Person"/>.</returns>
        IQueryable<Person> QueryAllPersons();
    }
}
