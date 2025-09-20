using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public IEnumerable<PersonDto> GetAllPersons()
        {
            IEnumerable<Person> people = personRepository.GetByHidden(false);
            return mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public PersonDto AddPerson(PersonDto dto)
        {
            Person person = mapper.Map<Person>(dto);
            Person addedPerson = personRepository.Add(person);
            personRepository.SaveChanges();
            return mapper.Map<PersonDto>(addedPerson);
        }

        public bool DeletePerson(int id)
        {
            Person? person = personRepository.FindById(id);
            if (person is null)
                return false;

            if(!person.Hidden)
            {
                person.Hidden = true;
                personRepository.SaveChanges();
            }
            return true;
        }

        public PersonDto? GetPersonById(int id)
        {
            Person? person = personRepository.FindById(id);
            if (person is null)
                return null;

            return mapper.Map<PersonDto>(person);
        }

        public PersonDto? UpdatePerson(int id, PersonDto dto)
        {
            Person? person = personRepository.FindById(id);
            if (person == null)
                return null;

            Person newPerson = mapper.Map<Person>(dto);
            newPerson.IdentificationNumber = person.IdentificationNumber; //zajistí aby bylo stejné IČO, které musí být neměnné
            newPerson.PersonId = 0;
            newPerson.Hidden = false;

            Person addedPerson = personRepository.Add(newPerson);
            person.Hidden = true;
            personRepository.SaveChanges();

            return mapper.Map<PersonDto>(addedPerson); 
        }
    }
}
