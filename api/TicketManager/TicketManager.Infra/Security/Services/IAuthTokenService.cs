namespace TicketManager.Infra.Security.Services
{
    public interface IAuthTokenService
    {
        public string Generate(string id, string name, string role);
    }
}
