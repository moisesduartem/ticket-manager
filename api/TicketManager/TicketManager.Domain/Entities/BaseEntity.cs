namespace TicketManager.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
