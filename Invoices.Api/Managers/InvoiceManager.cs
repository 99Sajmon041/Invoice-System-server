using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;
        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper, IPersonRepository personRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
            this.personRepository = personRepository;
        }
        public IEnumerable<InvoiceDto> GetAllInvoices(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3)
        {
            IEnumerable<Invoice> invoices = invoiceRepository.GetAllInvoicesWithDetails(buyerId, sellerId, product, minPrice, maxPrice, limit);
            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        public InvoiceDto AddInvoice(InvoiceCreateUpdateDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice createdInvoice = invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();

            return mapper.Map<InvoiceDto>(invoiceRepository.GetInvoiceWithDetails(createdInvoice.InvoiceId));
        }

        public bool DeleteInvoice(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
            {
                return false;
            }
            invoiceRepository.Delete(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }

        public InvoiceDto? GetInvoiceById(int id)
        {
            Invoice? invoice = invoiceRepository.GetInvoiceWithDetails(id);
            if (invoice is null)
                return null;

            return mapper.Map<InvoiceDto>(invoice);
        }

        public InvoiceDto? UpdateInvoice(int id, InvoiceCreateUpdateDto dto)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice == null)
                return null;

            mapper.Map(dto, invoice);
            invoiceRepository.SaveChanges();

            Invoice updatedInvoice = invoiceRepository.GetInvoiceWithDetails(id)!;
            return mapper.Map<InvoiceDto>(updatedInvoice);
        }

        public IEnumerable<InvoiceDto> GetInvoicesByIdentification(string identificationNumber, bool isBuyer, int limit = 3)
        {
            IEnumerable<Invoice>? invoices = new List<Invoice>();

            if (isBuyer)
                invoices = invoiceRepository.GetPurchasesByIdentification(identificationNumber.Trim(), limit);
            else
                invoices = invoiceRepository.GetSalesByIdentification(identificationNumber.Trim(), limit);

            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        public InvoiceStatisticsDto GetInvoiceStatistics()
        {
            IQueryable<Invoice> invoices = invoiceRepository.GetAllInvoices();

            return new InvoiceStatisticsDto
            {
                CurrentYearSum = invoices.Where(x => x.Issued.Year == DateTime.Now.Year).Sum(x => x.Price),
                AllTimeSum = invoices.Sum(x => x.Price),
                InvoicesCount = invoices.Count()
            };
        }
    }
}
