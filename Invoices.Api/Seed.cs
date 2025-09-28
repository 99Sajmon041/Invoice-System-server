using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Invoices.Api
{
    public static class Seed
    {
        public static async Task EnsureAsync(IServiceProvider sp, string adminEmail, string adminPassword)
        {
            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = sp.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleMgr.RoleExistsAsync(nameof(Roles.Admin)))
                await roleMgr.CreateAsync(new IdentityRole(nameof(Roles.Admin)));
            if (!await roleMgr.RoleExistsAsync(nameof(Roles.Client)))
                await roleMgr.CreateAsync(new IdentityRole(nameof(Roles.Client)));

            var admin = await userMgr.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new ApplicationUser
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var create = await userMgr.CreateAsync(admin, adminPassword);
            }

            if (admin is not null && !await userMgr.IsInRoleAsync(admin, nameof(Roles.Admin)))
            {
                await userMgr.AddToRoleAsync(admin, nameof(Roles.Admin));
            }
        }
    }
}
