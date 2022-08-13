namespace TicketManager.Api.Core.Domain.DTOs.Auth
{
    public class SignInDTO
    {
        public AuthUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
