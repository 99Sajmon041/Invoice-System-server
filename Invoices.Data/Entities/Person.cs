using Invoices.Data.Entities.Enums;

namespace Invoices.Data.Entities
{
    /// <summary>
    /// Entita představující osobu nebo firmu ve fakturačním systému
    /// Obsahuje všechny kontaktní a identifikační údaje
    /// </summary>
    public class Person
    {
        public int PersonId { get; set; }

        // Vlastnosti označené jako 'required' musí být nastaveny při vytváření instance (od .NET 7)
        public required string Name { get; set; }
        public required string IdentificationNumber { get; set; }
        public required string TaxNumber { get; set; }
        public required string AccountNumber { get; set; }
        public required string BankCode { get; set; }
        public required string Iban { get; set; }
        public required string Telephone { get; set; }
        public required string Mail { get; set; }
        public required string Street { get; set; }
        public required string Zip { get; set; }
        public required string City { get; set; }

        // Enum Country je v databázi uložen jako string (viz konfigurace v AppDbContext)
        public required Country Country { get; set; }

        public required string Note { get; set; }

        // Označení záznamu jako "skrytého" místo fyzického smazání (tzv. soft delete)
        public bool Hidden { get; set; }
    }
}