using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro správu faktur.
    /// Umožňuje listovat, vytvářet, číst, aktualizovat, mazat a získávat statistiky faktur.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceManager invoiceManager;

        /// <summary>
        /// Inicializuje novou instanci <see cref="InvoicesController"/>.
        /// </summary>
        /// <param name="invoiceManager">Aplikační služba pro práci s fakturami.</param>
        public InvoicesController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }

        /// <summary>
        /// Vrátí kolekci faktur dle zadaných filtrů.
        /// </summary>
        /// <param name="buyerId">Identifikátor kupujícího (volitelné).</param>
        /// <param name="sellerId">Identifikátor prodávajícího (volitelné).</param>
        /// <param name="product">Filtrovaný název produktu (volitelné; obsahuje).</param>
        /// <param name="minPrice">Minimální cena (volitelné).</param>
        /// <param name="maxPrice">Maximální cena (volitelné).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> odpovídající filtrům.</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<InvoiceDto>> GetAllInvoices(
            [FromQuery] int? buyerId,
            [FromQuery] int? sellerId,
            [FromQuery] string? product,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int limit = 3)
        {
            IEnumerable<InvoiceDto> invoices = invoiceManager.GetAllInvoices(buyerId, sellerId, product, minPrice, maxPrice, limit);
            return Ok(invoices);
        }

        /// <summary>
        /// Vytvoří novou fakturu.
        /// </summary>
        /// <param name="dto">Data pro vytvoření faktury.</param>
        /// <returns>HTTP 201 s vytvořenou fakturou a Location hlavičkou.</returns>
        [Authorize(Policy = nameof(Policy.CanWrite))]
        [HttpPost]
        public ActionResult AddInvoice([FromBody] InvoiceCreateUpdateDto dto)
        {
            InvoiceDto createdInvoice = invoiceManager.AddInvoice(dto);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.InvoiceId }, createdInvoice);
        }

        /// <summary>
        /// Vrátí detail faktury podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <returns><see cref="InvoiceDto"/> nebo HTTP 404, pokud nenalezena.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<InvoiceDto> GetInvoiceById(int id)
        {
            InvoiceDto? invoice = invoiceManager.GetInvoiceById(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        /// <summary>
        /// Aktualizuje existující fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <param name="dto">Data pro aktualizaci faktury.</param>
        /// <returns>Aktualizovaná faktura nebo HTTP 404, pokud nenalezena.</returns>
        [Authorize(Policy = nameof(Policy.CanWrite))]
        [HttpPut("{id}")]
        public ActionResult UpdateInvoiceById(int id, [FromBody] InvoiceCreateUpdateDto dto)
        {
            InvoiceDto? invoiceDto = invoiceManager.UpdateInvoice(id, dto);
            if (invoiceDto == null)
                return NotFound();

            return Ok(invoiceDto);
        }

        /// <summary>
        /// Odstraní fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <returns>HTTP 204 při úspěchu nebo HTTP 404, pokud nenalezena.</returns>
        [Authorize(Policy = nameof(Policy.CanDelete))]
        [HttpDelete("{id}")]
        public ActionResult DeleteInvoiceById(int id)
        {
            if (!invoiceManager.DeleteInvoice(id))
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Vrátí souhrnné statistiky nad fakturami.
        /// </summary>
        /// <returns><see cref="InvoiceStatisticsDto"/> se souhrnnými hodnotami.</returns>
        [AllowAnonymous]
        [HttpGet("statistics")]
        public ActionResult<InvoiceStatisticsDto> GetStatistics()
        {
            InvoiceStatisticsDto invoiceStats = invoiceManager.GetInvoiceStatistics();
            return Ok(invoiceStats);
        }
    }
}
