namespace Invoices.Api.Models
{
    public class PersonStatisticsDto
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; } = "";
        public decimal Revenue { get; set; }
    }
}
