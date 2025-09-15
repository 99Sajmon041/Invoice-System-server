using Invoices.Data.Entities.Enums;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) třída pro přenos dat o osobě přes API.
    /// Odděluje vnější datovou strukturu (např. JSON pro frontend) od vnitřní databázové entity.
    /// </summary>
    public class PersonDto
    {
        // Upravíme název vlastnosti v JSON výstupu (např. "_id" místo "PersonId") kvůli React klientovi
        [JsonPropertyName("_id")]
        public int PersonId { get; set; }

        public string Name { get; set; } = "";
        public string IdentificationNumber { get; set; } = "";
        public string TaxNumber { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public string BankCode { get; set; } = "";
        public string Iban { get; set; } = "";
        public string Telephone { get; set; } = "";
        public string Mail { get; set; } = "";
        public string Street { get; set; } = "";
        public string Zip { get; set; } = "";
        public string City { get; set; } = "";

        // Enum Country se v JSONu serializuje jako string
        public Country Country { get; set; }

        public string Note { get; set; } = "";
    }
}