using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Repozitář pro entitu <see cref="Person"/> poskytující přístup k datům osob.
    /// </summary>
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        /// <summary>
        /// Inicializuje novou instanci <see cref="PersonRepository"/> s daným databázovým kontextem.
        /// </summary>
        /// <param name="context">Aplikační databázový kontext.</param>
        public PersonRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Vrátí kolekci osob dle příznaku skrytí.
        /// </summary>
        /// <param name="hidden">Příznak určující, zda má být osoba skryta.</param>
        /// <returns>Kolekce entit <see cref="Person"/> odpovídajících filtru.</returns>
        public IEnumerable<Person> GetByHidden(bool hidden)
        {
            return dbSet.AsNoTracking().Where(x => x.Hidden == hidden).ToList();
        }

        /// <summary>
        /// Vytvoří dotaz nad všemi osobami s režimem bez sledování změn.
        /// </summary>
        /// <returns><see cref="IQueryable{T}"/> pro entitu <see cref="Person"/> vhodné pro další kompozici dotazů.</returns>
        public IQueryable<Person> QueryAllPersons()
        {
            return dbSet.AsNoTracking();
        }
    }
}
