using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro práci s osobami (Person).
    /// Přijímá HTTP požadavky a deleguje logiku do <see cref="IPersonManager"/>.
    /// Controller neřeší byznys logiku, pouze zpracovává vstup a vrací odpovědi.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonManager personManager;

        /// <summary>
        /// Vytváří novou instanci kontroleru a nastavuje závislost na <see cref="IPersonManager"/>.
        /// </summary>
        /// <param name="personManager">Manager, který zajišťuje veškerou logiku práce s osobami.</param>
        public PersonsController(IPersonManager personManager)
        {
            this.personManager = personManager;
        }

        /// <summary>
        /// Vrátí seznam všech osob v systému.
        /// </summary>
        /// <returns>
        /// HTTP 200 (OK) s kolekcí <see cref="PersonDto"/> reprezentující všechny osoby.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAllPersons()
        {
            IEnumerable<PersonDto> people = personManager.GetAllPersons();
            return Ok(people);
        }

        /// <summary>
        /// Vytvoří novou osobu podle dodaného DTO.
        /// </summary>
        /// <param name="dto">Data nové osoby.</param>
        /// <returns>
        /// HTTP 201 (Created) s reprezentací nově vytvořené osoby.
        /// </returns>
        [HttpPost]
        public ActionResult<PersonDto> AddPerson([FromBody] PersonDto dto)
        {
            PersonDto createdPerson = personManager.AddPerson(dto);

            // V budoucnu: až bude implementován detail (GET /api/persons/{id}),
            // lze vracet odkaz na nově vytvořený záznam v location hlavičce:
            // return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

            return Created(string.Empty, createdPerson);
        }

        /// <summary>
        /// Smaže osobu podle jejího ID.
        /// </summary>
        /// <param name="id">ID osoby ke smazání.</param>
        /// <returns>
        /// HTTP 204 (NoContent), pokud byla osoba úspěšně odstraněna,
        /// nebo HTTP 404 (NotFound), pokud osoba neexistuje.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            bool success = personManager.DeletePerson(id);

            if (!success)
                return NotFound(); // HTTP 404 – pokud osoba neexistuje

            return NoContent(); // HTTP 204 – smazáno, server nic dále nevrací
        }

        /// <summary>
        /// Vrací osobou na základě Id, pokue neexistuje, tak NotFound()
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<PersonDto> GetPersonById(int id)
        {
            PersonDto? personDto = personManager.GetPersonById(id);
            if(personDto is null)
                return NotFound();
            
            return Ok(personDto);
        }
    }
}
