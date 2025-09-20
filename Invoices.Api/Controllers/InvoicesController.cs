using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceManager invoiceManager;

        public InvoicesController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvoiceDto>> GetAllInvoices(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3)
        {
            IEnumerable<InvoiceDto> invoices = invoiceManager.GetAllInvoices(buyerId, sellerId, product, minPrice, maxPrice, limit);
            return Ok(invoices);
        }

        [HttpPost]
        public ActionResult AddInvoice([FromBody] InvoiceCreateUpdateDto dto)
        {
            InvoiceDto createdInvoice = invoiceManager.AddInvoice(dto);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.InvoiceId }, createdInvoice);
        }
        
        [HttpGet("{id}")]
        public ActionResult<InvoiceDto> GetInvoiceById(int id)
        {
            InvoiceDto? invoice = invoiceManager.GetInvoiceById(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateInvoiceById(int id, [FromBody] InvoiceCreateUpdateDto dto)
        {
            InvoiceDto? invoiceDto = invoiceManager.UpdateInvoice(id, dto);
            if (invoiceDto == null)
                return NotFound();

            return Ok(invoiceDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteInvoiceById(int id)
        {
            if (!invoiceManager.DeleteInvoice(id))
                return NotFound();

            return NoContent();
        }
    }
}
