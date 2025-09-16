using AutoMapper;
using Invoices.Api.Models;
using Invoices.Data.Entities;

namespace Invoices.Api
{
    /// <summary>
    /// Profil pro AutoMapper – definuje, jak se mají převádět objekty mezi entitami a DTO třídami.
    /// Pomáhá oddělit databázovou vrstvu (Entity) od API vrsty (DTO)
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ReverseMap() vytvoří i opačné mapování (DTO -> Entity i Entity -> DTO)
            CreateMap<Person, PersonDto>().ReverseMap();

            // Mapování z InvoiceDto do Invoice entity
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(e => e.Buyer, o => o.Ignore())
                .ForMember(e => e.Seller, o => o.Ignore());

            // Mapování Invoice do InvoiceDto DTO modelu
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(d => d.BuyerName, o => o.MapFrom(s => s.Buyer.Name))
                .ForMember(d => d.SellerName, o => o.MapFrom(s => s.Seller.Name));
        }
    }
}