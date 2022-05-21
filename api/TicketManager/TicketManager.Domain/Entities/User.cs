using TicketManager.Domain.Enums;

namespace TicketManager.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Hash { get; private set; }
        public string Salt { get; private set; }
        public UserRole Role { get; private set; }

        public bool IsAdmin => Role is UserRole.Admin;

        public User(string name, string hash, string salt)
        {
            Name = name;
            Hash = hash;
            Salt = salt;
        }

        public User(int id, string name, string hash, string salt, UserRole role)
        {
            Id = id;
            Name = name;
            Hash = hash;
            Salt = salt;
            Role = role;
        }
    }
}
