namespace TicketManager.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

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
