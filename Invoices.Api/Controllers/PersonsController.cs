using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonManager personManager;

        public PersonsController(IPersonManager personManager)
        {
            this.personManager = personManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAllPersons()
        {
            IEnumerable<PersonDto> people = personManager.GetAllPersons();
            return Ok(people);
        }

        [HttpPost]
        public ActionResult<PersonDto> AddPerson([FromBody] PersonDto dto)
        {
            dto.PersonId = 0;
            PersonDto createdPerson = personManager.AddPerson(dto);
            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.PersonId }, createdPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            return personManager.DeletePerson(id) ? NoContent() : NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<PersonDto> GetPersonById(int id)
        {
            PersonDto? personDto = personManager.GetPersonById(id);
            if(personDto is null)
                return NotFound();
            
            return Ok(personDto);
        }

        [HttpPut("{id}")]
        public ActionResult<PersonDto> UpdatePersonById(int id, [FromBody] PersonDto dto)
        {
            PersonDto? updated = personManager.UpdatePerson(id, dto);   
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }
    }
}
