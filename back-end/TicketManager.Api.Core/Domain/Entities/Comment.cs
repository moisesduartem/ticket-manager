namespace TicketManager.Api.Core.Domain.Entities
{
    public class Comment : Entity
    {
        public string Content { get; private set; }
        public int AuthorId { get; private set; }
        public int TicketId { get; private set; }
        public User Author { get; set; }
        public Ticket Ticket { get; set; }

        public Comment(string content, int authorId, int ticketId)
        {
            Content = content;
            AuthorId = authorId;
            TicketId = ticketId;
        }

        public Comment(int id, string content, int authorId, int ticketId)
        {
            Id = id;
            Content = content;
            AuthorId = authorId;
            TicketId = ticketId;
        }
    }
}
