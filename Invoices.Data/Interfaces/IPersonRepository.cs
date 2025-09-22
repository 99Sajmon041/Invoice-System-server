using Invoices.Data.Entities;

namespace Invoices.Data.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        IEnumerable<Person> GetByHidden(bool hidden);
        IQueryable<Person> QueryAllPersons();
    }
}
