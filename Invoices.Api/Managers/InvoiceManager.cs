using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Aplikační služba pro správu faktur. Zajišťuje mapování mezi entitami a DTO a využívá repozitáře datové vrstvy.
    /// </summary>
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Inicializuje novou instanci <see cref="InvoiceManager"/>.
        /// </summary>
        /// <param name="invoiceRepository">Repozitář pro práci s fakturami.</param>
        /// <param name="mapper">Služba AutoMapper pro mapování entit a DTO.</param>
        /// <param name="personRepository">Repozitář pro práci s osobami.</param>
        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper, IPersonRepository personRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
            this.personRepository = personRepository;
        }

        /// <summary>
        /// Načte kolekci faktur dle zadaných filtrů.
        /// </summary>
        /// <param name="buyerId">Identifikátor kupujícího (volitelné).</param>
        /// <param name="sellerId">Identifikátor prodávajícího (volitelné).</param>
        /// <param name="product">Filtrovaný název produktu (volitelné; podmínka obsahuje).</param>
        /// <param name="minPrice">Minimální cena (volitelné).</param>
        /// <param name="maxPrice">Maximální cena (volitelné).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> odpovídající filtrům.</returns>
        public IEnumerable<InvoiceDto> GetAllInvoices(int? buyerId, int? sellerId, string? product, decimal? minPrice, decimal? maxPrice, int limit = 3)
        {
            IEnumerable<Invoice> invoices = invoiceRepository.GetAllInvoicesWithDetails(buyerId, sellerId, product, minPrice, maxPrice, limit);
            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        /// <summary>
        /// Vytvoří novou fakturu.
        /// </summary>
        /// <param name="dto">Data pro vytvoření faktury.</param>
        /// <returns>Vytvořená faktura jako <see cref="InvoiceDto"/>.</returns>
        public InvoiceDto AddInvoice(InvoiceCreateUpdateDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice createdInvoice = invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();

            return mapper.Map<InvoiceDto>(invoiceRepository.GetInvoiceWithDetails(createdInvoice.InvoiceId));
        }

        /// <summary>
        /// Odstraní fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <returns><c>true</c>, pokud byla faktura odstraněna; jinak <c>false</c>.</returns>
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

        /// <summary>
        /// Načte fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <returns><see cref="InvoiceDto"/> nebo <c>null</c>, pokud faktura neexistuje.</returns>
        public InvoiceDto? GetInvoiceById(int id)
        {
            Invoice? invoice = invoiceRepository.GetInvoiceWithDetails(id);
            if (invoice is null)
                return null;

            return mapper.Map<InvoiceDto>(invoice);
        }

        /// <summary>
        /// Aktualizuje fakturu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor faktury.</param>
        /// <param name="dto">Data pro aktualizaci faktury.</param>
        /// <returns>Aktualizovaná faktura jako <see cref="InvoiceDto"/> nebo <c>null</c>, pokud nebyla nalezena.</returns>
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

        /// <summary>
        /// Načte kolekci faktur podle identifikačního čísla subjektu a jeho role vůči faktuře.
        /// </summary>
        /// <param name="identificationNumber">Identifikační číslo subjektu.</param>
        /// <param name="subject">Role subjektu (kupující/prodávající).</param>
        /// <param name="limit">Maximální počet vrácených položek.</param>
        /// <returns>Kolekce <see cref="InvoiceDto"/> odpovídající podmínkám.</returns>
        public IEnumerable<InvoiceDto> GetInvoicesByIdentification(string identificationNumber, Subject subject, int limit = 3)
        {
            IEnumerable<Invoice>? invoices = new List<Invoice>();

            if (subject == Subject.Buyer)
                invoices = invoiceRepository.GetInvoicesBySubject(Subject.Buyer, identificationNumber.Trim(), limit);
            else
                invoices = invoiceRepository.GetInvoicesBySubject(Subject.Seller, identificationNumber.Trim(), limit);

            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        /// <summary>
        /// Vrátí agregované statistiky nad fakturami.
        /// </summary>
        /// <returns><see cref="InvoiceStatisticsDto"/> se souhrnnými hodnotami.</returns>
        public InvoiceStatisticsDto GetInvoiceStatistics()
        {
            IQueryable<Invoice> invoices = invoiceRepository.GetAllInvoices();

            return new InvoiceStatisticsDto
            {
                /// <summary>
                /// Součet částek všech faktur v aktuálním roce.
                /// </summary>
                CurrentYearSum = invoices.Where(x => x.Issued.Year == DateTime.Now.Year).Sum(x => x.Price),

                /// <summary>
                /// Součet částek všech faktur za celé období.
                /// </summary>
                AllTimeSum = invoices.Sum(x => x.Price),

                /// <summary>
                /// Celkový počet faktur.
                /// </summary>
                InvoicesCount = invoices.Count()
            };
        }
    }
}
