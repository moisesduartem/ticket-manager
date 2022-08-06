namespace TicketManager.Shared.DTOs.Tickets
{
    public class CreateTicketRequest : IAuthorRequest
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
