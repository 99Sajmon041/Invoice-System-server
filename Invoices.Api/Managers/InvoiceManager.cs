using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using System.Collections.Generic;

namespace Invoices.Api.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;
        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }
        public IEnumerable<InvoiceDto> GetAllInvoices()
        {
            IEnumerable<Invoice> invoices = invoiceRepository.GetAll();
            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        public InvoiceDto AddInvoice(InvoiceDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice addedInvoice = invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();
            return mapper.Map<InvoiceDto>(addedInvoice);
        }

        public bool DeleteInvoice(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
            {
                return false;
            }
            invoiceRepository.Delete(invoice);
            return true;
        }

        public InvoiceDto? GetPersonById(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return null;

            return mapper.Map<InvoiceDto>(invoice);
        }
    }
}
