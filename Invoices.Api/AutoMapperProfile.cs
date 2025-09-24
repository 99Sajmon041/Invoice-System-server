using AutoMapper;
using Invoices.Api.Models;
using Invoices.Data.Entities;

namespace Invoices.Api
{
    /// <summary>
    /// AutoMapper profil definující mapování mezi entitami datové vrstvy a DTO modely API.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Inicializuje konfigurace mapování.
        /// </summary>
        public AutoMapperProfile()
        {
            // Person ↔ PersonDto
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>()
                .ForMember(d => d.PersonId, opt => opt.Ignore())
                .ForMember(d => d.Hidden, opt => opt.Ignore());

            // Invoice ↔ InvoiceDto
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(e => e.Buyer, o => o.Ignore())
                .ForMember(e => e.Seller, o => o.Ignore());

            // InvoiceCreateUpdateDto → Invoice
            CreateMap<InvoiceCreateUpdateDto, Invoice>()
                .ForMember(d => d.InvoiceId, o => o.Ignore())
                .ForMember(d => d.BuyerId, o => o.MapFrom(s => s.Buyer.PersonId))
                .ForMember(d => d.SellerId, o => o.MapFrom(s => s.Seller.PersonId))
                .ForMember(d => d.Seller, o => o.Ignore())
                .ForMember(d => d.Buyer, o => o.Ignore());
        }
    }
}
