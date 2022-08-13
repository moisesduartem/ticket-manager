using TicketManager.Api.Core.Domain.Enums;

namespace TicketManager.Api.Core.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Hash { get; private set; }
        public string Salt { get; private set; }
        public UserRole Role { get; private set; }
        public List<Ticket> Tickets { get; set; }
        public List<Comment> Comments { get; set; }

        public bool IsAdmin => Role is UserRole.Admin;
        public string RoleName => Enum.GetName(Role);

        public User(string name, string email, string hash, string salt)
        {
            Name = name;
            Email = email;
            Hash = hash;
            Salt = salt;
        }

        public User(int id, string name, string email, string hash, string salt, UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            Hash = hash;
            Salt = salt;
            Role = role;
        }
    }
}
