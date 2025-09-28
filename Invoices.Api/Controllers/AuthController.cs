using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        IJwtTokenService jwt;
        IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenService jwt,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwt = jwt;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existing = await userManager.FindByEmailAsync(dto.Email);
            if (existing is not null)
            {
                ModelState.AddModelError("", "E-Mail je již registrován");
                return ValidationProblem(ModelState);
            }

            var user = new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true
            };

            var create = await userManager.CreateAsync(user, dto.Password);
            if(!create.Succeeded)
            {
                foreach (var error in create.Errors)
                    ModelState.AddModelError("", error.Description);

                return ValidationProblem(ModelState);
            }

            await userManager.AddToRoleAsync(user, nameof(Roles.Client));

            var token = await jwt.CreateAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var minutes = int.TryParse(configuration["Jwt:ExpiresMinutes"], out var m) ? m : 120;

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                Roles = roles,
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(minutes)
            });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var user = await userManager.FindByNameAsync(dto.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Nesprávný E-mail nebo heslo.");
                return ValidationProblem(ModelState);
            }

            var signIn = await signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
            if(!signIn.Succeeded)
            {
                ModelState.AddModelError("", "Nesprávný E-mail nebo heslo.");
                return ValidationProblem(ModelState);
            }

            var token = await jwt.CreateAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var minutes = int.TryParse(configuration["Jwt:ExpiresMinutes"], out var m) ? m : 120;
            
            return Ok(new AuthResponseDto()
            {
                Token = token,
                Email = user.Email!,
                Roles = roles,
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(minutes)
            });
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
