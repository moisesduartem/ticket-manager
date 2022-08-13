namespace TicketManager.Api.Core.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public List<Ticket> Tickets { get; set; }

        public Category(string name)
        {
            Name = name;
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
