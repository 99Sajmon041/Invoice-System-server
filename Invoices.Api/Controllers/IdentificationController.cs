using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Api.Controllers
{
    /// <summary>
    /// API kontroler pro načítání faktur podle identifikačního čísla subjektu.
    /// Umožňuje získat prodeje i nákupy pro dané IČ.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class IdentificationController : ControllerBase
    {
        private readonly IInvoiceManager invoiceManager;

        /// <summary>
        /// Inicializuje novou instanci <see cref="IdentificationController"/>.
        /// </summary>
        /// <param name="invoiceManager">Služba pro práci s fakturami.</param>
        public IdentificationController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }

        /// <summary>
        /// Vrátí seznam faktur, kde dané identifikační číslo vystupuje jako prodávající (prodeje).
        /// </summary>
        /// <param name="identificationNumber">Identifikační číslo subjektu.</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> prodejních faktur.</returns>

        [HttpGet("{identificationNumber}/sales")]
        public ActionResult<IEnumerable<InvoiceDto>> Sales(
            [FromRoute] string identificationNumber,
            [FromQuery, Range(1, 20)] int limit = 3)
        {
            return Ok(invoiceManager.GetInvoicesByIdentification(identificationNumber, Subject.Seller, limit).ToList());
        }

        /// <summary>
        /// Vrátí seznam faktur, kde dané identifikační číslo vystupuje jako kupující (nákupy).
        /// </summary>
        /// <param name="identificationNumber">Identifikační číslo subjektu.</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> nákupních faktur.</returns>
        [HttpGet("{identificationNumber}/purchases")]
        public ActionResult<IEnumerable<InvoiceDto>> Purchases(
            [FromRoute] string identificationNumber,
            [FromQuery, Range(1, 20)] int limit = 3)
        {
            return Ok(invoiceManager.GetInvoicesByIdentification(identificationNumber, Subject.Buyer, limit).ToList());
        }
    }
}
