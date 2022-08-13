namespace TicketManager.Api.Core.Domain.Entities
{
    public class Ticket : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int AuthorId { get; private set; }
        public int CategoryId { get; private set; }
        public bool IsSolved { get; private set; }

        public User Author { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }

        public Ticket(string title, int authorId, int categoryId, string description = "")
        {
            Title = title;
            AuthorId = authorId;
            CategoryId = categoryId;
            Description = description;
            IsSolved = false;
        }

        public Ticket(int id, string title, string description, int authorId, int categoryId, bool isSolved)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorId = authorId;
            CategoryId = categoryId;
            IsSolved = isSolved;
        }
    }
}
