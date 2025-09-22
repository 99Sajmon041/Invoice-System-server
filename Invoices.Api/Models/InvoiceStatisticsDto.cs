namespace Invoices.Api.Models
{
    public class InvoiceStatisticsDto
    {
        public decimal CurrentYearSum { get; set; }
        public decimal AllTimeSum { get; set; }
        public int InvoicesCount { get; set; }
    }
}
