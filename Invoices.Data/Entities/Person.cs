using Invoices.Data.Entities.Enums;

namespace Invoices.Data.Entities
{
    /// <summary>
    /// Reprezentuje osobu (subjekt) v systému fakturace.
    /// Obsahuje identifikační, kontaktní a adresní údaje a navigační vlastnosti na nákupy a prodeje.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Primární klíč osoby.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Název/jméno osoby.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Identifikační číslo osoby (např. IČ).
        /// </summary>
        public required string IdentificationNumber { get; set; }

        /// <summary>
        /// Daňové identifikační číslo (např. DIČ).
        /// </summary>
        public required string TaxNumber { get; set; }

        /// <summary>
        /// Číslo bankovního účtu.
        /// </summary>
        public required string AccountNumber { get; set; }

        /// <summary>
        /// Kód banky.
        /// </summary>
        public required string BankCode { get; set; }

        /// <summary>
        /// Mezinárodní číslo bankovního účtu (IBAN).
        /// </summary>
        public required string Iban { get; set; }

        /// <summary>
        /// Telefonní číslo.
        /// </summary>
        public required string Telephone { get; set; }

        /// <summary>
        /// E-mailová adresa.
        /// </summary>
        public required string Mail { get; set; }

        /// <summary>
        /// Ulice a číslo popisné.
        /// </summary>
        public required string Street { get; set; }

        /// <summary>
        /// PSČ.
        /// </summary>
        public required string Zip { get; set; }

        /// <summary>
        /// Město.
        /// </summary>
        public required string City { get; set; }

        /// <summary>
        /// Stát.
        /// </summary>
        public required Country Country { get; set; }

        /// <summary>
        /// Poznámka k osobě.
        /// </summary>
        public required string Note { get; set; }

        /// <summary>
        /// Příznak skrytí osoby.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Navigační kolekce nákupů (faktur, kde je osoba kupující).
        /// </summary>
        public ICollection<Invoice> Purchases { get; set; } = new List<Invoice>();

        /// <summary>
        /// Navigační kolekce prodejů (faktur, kde je osoba prodávající).
        /// </summary>
        public ICollection<Invoice> Sales { get; set; } = new List<Invoice>();
    }
}
