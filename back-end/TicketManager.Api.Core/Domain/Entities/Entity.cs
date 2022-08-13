namespace TicketManager.Api.Core.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Entity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
