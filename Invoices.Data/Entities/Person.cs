using Invoices.Data.Entities.Enums;

namespace Invoices.Data.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
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
        public required Country Country { get; set; }
        public required string Note { get; set; }
        public bool Hidden { get; set; }
        public ICollection<Invoice>? Purchases { get; set; }
        public ICollection<Invoice>? Sales { get; set; }
    }
}