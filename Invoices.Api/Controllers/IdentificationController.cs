using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class IdentificationController : ControllerBase
    {
        private readonly IInvoiceManager invoiceManager;

        public IdentificationController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }

        [HttpGet("{identificationNumber}/sales")]
        public ActionResult<IEnumerable<InvoiceDto>> Sales([FromRoute] string identificationNumber, [FromQuery] int limit = 3)
        {
            return Ok(invoiceManager.GetInvoicesByIdentification(identificationNumber, false, limit).ToList());
        }

        [HttpGet("{identificationNumber}/purchases")]
        public ActionResult<IEnumerable<InvoiceDto>> Purchases([FromRoute] string identificationNumber, [FromQuery, Range(1, 20)] int limit = 3)
        {
            return Ok(invoiceManager.GetInvoicesByIdentification(identificationNumber, true, limit).ToList());
        }
    }
}
