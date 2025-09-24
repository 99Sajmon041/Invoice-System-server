namespace Invoices.Api.Models
{
    /// <summary>
    /// Reprezentuje statistické údaje o osobě pro účely přehledů a reportů.
    /// </summary>
    public class PersonStatisticsDto
    {
        /// <summary>
        /// Identifikátor osoby.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Název nebo jméno osoby.
        /// </summary>
        public string PersonName { get; set; } = "";

        /// <summary>
        /// Celkové tržby (výnos) spojené s osobou.
        /// </summary>
        public decimal Revenue { get; set; }
    }
}
