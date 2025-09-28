using Invoices.Data.Entities;

namespace Invoices.Api.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateAsync(ApplicationUser user);
    }
}
