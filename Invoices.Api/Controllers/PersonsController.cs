using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro správu osob.
    /// Umožňuje listovat, vytvářet, číst, aktualizovat a skrývat osoby.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonManager personManager;

        /// <summary>
        /// Inicializuje novou instanci <see cref="PersonsController"/>.
        /// </summary>
        /// <param name="personManager">Aplikační služba pro práci s osobami.</param>
        public PersonsController(IPersonManager personManager)
        {
            this.personManager = personManager;
        }

        /// <summary>
        /// Vrátí všechny neskryté osoby.
        /// </summary>
        /// <returns>Kolekce <see cref="PersonDto"/>.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAllPersons()
        {
            IEnumerable<PersonDto> people = personManager.GetAllPersons();
            return Ok(people);
        }

        /// <summary>
        /// Vytvoří novou osobu.
        /// </summary>
        /// <param name="dto">Data osoby.</param>
        /// <returns>HTTP 201 s vytvořenou osobou a Location hlavičkou.</returns>
        [HttpPost]
        public ActionResult<PersonDto> AddPerson([FromBody] PersonDto dto)
        {
            dto.PersonId = 0;
            PersonDto createdPerson = personManager.AddPerson(dto);
            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.PersonId }, createdPerson);
        }

        /// <summary>
        /// Skryje osobu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns>HTTP 204 při úspěchu nebo HTTP 404, pokud nenalezena.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            return personManager.DeletePerson(id) ? NoContent() : NotFound();
        }

        /// <summary>
        /// Vrátí detail osoby podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor osoby.</param>
        /// <returns><see cref="PersonDto"/> nebo HTTP 404, pokud nenalezena.</returns>
        [HttpGet("{id}")]
        public ActionResult<PersonDto> GetPersonById(int id)
        {
            PersonDto? personDto = personManager.GetPersonById(id);
            if (personDto is null)
                return NotFound();

            return Ok(personDto);
        }

        /// <summary>
        /// Aktualizuje osobu vytvořením nové verze a skrytím původní.
        /// </summary>
        /// <param name="id">Identifikátor původní osoby.</param>
        /// <param name="dto">Aktualizovaná data osoby.</param>
        /// <returns>Aktualizovaná osoba nebo HTTP 404, pokud původní osoba nebyla nalezena.</returns>
        [HttpPut("{id}")]
        public ActionResult<PersonDto> UpdatePersonById(int id, [FromBody] PersonDto dto)
        {
            PersonDto? updated = personManager.UpdatePerson(id, dto);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        /// <summary>
        /// Vrátí statistiky osob (identifikátor, název a tržby).
        /// </summary>
        /// <returns>Kolekce <see cref="PersonStatisticsDto"/>.</returns>
        [HttpGet("statistics")]
        public ActionResult<IEnumerable<PersonStatisticsDto>> GetPersonStatistics()
        {
            IEnumerable<PersonStatisticsDto> personStatistics = personManager.GetPersonStatistics();
            return Ok(personStatistics);
        }
    }
}
