using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Aplikační služba pro správu osob. Zajišťuje mapování mezi entitami a DTO a využívá repozitář datové vrstvy.
    /// </summary>
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Inicializuje novou instanci <see cref="PersonManager"/>.
        /// </summary>
        /// <param name="personRepository">Repozitář pro práci s osobami.</param>
        /// <param name="mapper">Služba AutoMapper pro mapování entit a DTO.</param>
        public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Načte všechny neskryté osoby.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonDto"/>.</returns>
        public IEnumerable<PersonDto> GetAllPersons()
        {
            IEnumerable<Person> people = personRepository.GetByHidden(false);
            return mapper.Map<IEnumerable<PersonDto>>(people);
        }

        /// <summary>
        /// Vytvoří novou osobu.
        /// </summary>
        /// <param name="dto">Data osoby.</param>
        /// <returns>Vytvořená osoba jako <see cref="PersonDto"/>.</returns>
        public PersonDto AddPerson(PersonDto dto)
        {
            Person person = mapper.Map<Person>(dto);
            Person addedPerson = personRepository.Add(person);
            personRepository.SaveChanges();
            return mapper.Map<PersonDto>(addedPerson);
        }

        /// <summary>
        /// Skryje osobu označením příznaku Hidden.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><c>true</c>, pokud byla osoba nalezena; jinak <c>false</c>.</returns>
        public bool DeletePerson(int id)
        {
            Person? person = personRepository.FindById(id);
            if (person is null)
                return false;

            if (!person.Hidden)
            {
                person.Hidden = true;
                personRepository.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Načte osobu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><see cref="PersonDto"/> nebo <c>null</c>, pokud neexistuje.</returns>
        public PersonDto? GetPersonById(int id)
        {
            Person? person = personRepository.FindById(id);
            if (person is null)
                return null;

            return mapper.Map<PersonDto>(person);
        }

        /// <summary>
        /// Aktualizuje osobu vytvořením nové verze a skrytím původní.
        /// </summary>
        /// <param name="id">Identifikátor původní osoby.</param>
        /// <param name="dto">Aktualizovaná data osoby.</param>
        /// <returns>Aktualizovaná osoba jako <see cref="PersonDto"/> nebo <c>null</c>, pokud původní osoba nebyla nalezena.</returns>
        public PersonDto? UpdatePerson(int id, PersonDto dto)
        {
            Person? person = personRepository.FindById(id);
            if (person == null)
                return null;

            Person newPerson = mapper.Map<Person>(dto);
            newPerson.IdentificationNumber = person.IdentificationNumber;
            newPerson.PersonId = 0;
            newPerson.Hidden = false;

            Person addedPerson = personRepository.Add(newPerson);
            person.Hidden = true;
            personRepository.SaveChanges();

            return mapper.Map<PersonDto>(addedPerson);
        }

        /// <summary>
        /// Vrátí statistiky osob zahrnující identifikátor, název a tržby z prodejů.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonStatisticsDto"/>.</returns>
        public IEnumerable<PersonStatisticsDto> GetPersonStatistics()
        {
            IQueryable<Person> persons = personRepository.QueryAllPersons();
            persons = persons.Where(x => !x.Hidden);

            return persons.Select(x => new PersonStatisticsDto
            {
                PersonId = x.PersonId,
                PersonName = x.Name,
                Revenue = x.Sales.Sum(i => (decimal?)i.Price) ?? 0m
            })
            .ToList();
        }
    }
}
