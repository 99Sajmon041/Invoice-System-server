using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Person> GetByHidden(bool hidden)
        {
            return dbSet.AsNoTracking().Where(x => x.Hidden == hidden).ToList();
        }

        public IQueryable<Person> QueryAllPersons()
        {
            return dbSet.AsNoTracking();
        }
    }
}