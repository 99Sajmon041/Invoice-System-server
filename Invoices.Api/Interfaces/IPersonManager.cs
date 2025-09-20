using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    public interface IPersonManager
    {
        IEnumerable<PersonDto> GetAllPersons();
        PersonDto AddPerson(PersonDto dto);
        bool DeletePerson(int id);
        PersonDto? GetPersonById(int id);
        PersonDto? UpdatePerson(int id, PersonDto dto);
    }
}
