using Invoices.Data.Entities;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Specializované rozhraní pro repozitář pracující s entitou <see cref="Person"/>.
    /// Rozšiřuje obecné metody z <see cref="IRepository{T}"/> o specifické funkce pro osoby.
    /// </summary>
    public interface IPersonRepository : IBaseRepository<Person>
    {
        /// <summary>
        /// Vrátí všechny osoby podle hodnoty příznaku <see cref="Person.Hidden"/>.
        /// Slouží pro práci se skrytými (soft-deleted) záznamy.
        /// </summary>
        /// <param name="hidden">Hodnota příznaku Hidden (true = skryté osoby, false = viditelné).</param>
        /// <returns>Kolekce osob odpovídajících podmínce.</returns>
        IEnumerable<Person> GetByHidden(bool hidden);
    }
}
