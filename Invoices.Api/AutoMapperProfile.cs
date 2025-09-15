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
        }
    }
}