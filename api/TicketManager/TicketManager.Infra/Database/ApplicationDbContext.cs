using Microsoft.EntityFrameworkCore;
using TicketManager.Domain.Entities;

namespace TicketManager.Infra.Database
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly string ConnectionString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
