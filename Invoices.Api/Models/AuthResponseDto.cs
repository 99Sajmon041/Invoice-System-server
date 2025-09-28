namespace Invoices.Api.Models
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = "";
        public string Email { get; set; } = "";
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
        public DateTime ExpiresAtUtc { get; set; }
    }
}
