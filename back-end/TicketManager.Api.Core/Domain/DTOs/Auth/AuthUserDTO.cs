namespace TicketManager.Api.Core.Domain.DTOs.Auth
{
    public class AuthUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
