using Invoices.Api.Interfaces;
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
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
