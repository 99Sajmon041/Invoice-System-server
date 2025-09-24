namespace Invoices.Api.Models
{
    /// <summary>
    /// Reprezentuje statistické údaje o fakturách pro účely přehledů a reportů.
    /// </summary>
    public class InvoiceStatisticsDto
    {
        /// <summary>
        /// Součet částek všech faktur v aktuálním roce.
        /// </summary>
        public decimal CurrentYearSum { get; set; }

        /// <summary>
        /// Součet částek všech faktur za celé období.
        /// </summary>
        public decimal AllTimeSum { get; set; }

        /// <summary>
        /// Celkový počet faktur.
        /// </summary>
        public int InvoicesCount { get; set; }
    }
}
