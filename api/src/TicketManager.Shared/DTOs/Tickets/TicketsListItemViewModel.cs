namespace TicketManager.Shared.DTOs.Tickets
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AuthorViewModel Author { get; set; }
        public CategoryViewModel Category { get; set; }
        public bool IsSolved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
