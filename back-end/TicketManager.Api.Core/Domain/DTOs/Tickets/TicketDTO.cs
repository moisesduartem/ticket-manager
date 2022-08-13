namespace TicketManager.Api.Core.Domain.DTOs.Tickets
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AuthorDTO Author { get; set; }
        public CategoryDTO Category { get; set; }
        public bool IsSolved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
