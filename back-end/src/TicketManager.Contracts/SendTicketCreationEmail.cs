using TicketManager.Contracts.Entities;

namespace TicketManager.Contracts
{
    public class SendTicketCreationEmail
    {
        public int TicketId { get; set; }
        public TicketAuthor Author { get; set; }
    }
}