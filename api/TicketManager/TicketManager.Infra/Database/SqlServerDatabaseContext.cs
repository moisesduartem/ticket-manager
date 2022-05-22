using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicketManager.Domain.Entities;
using TicketManager.Shared.Infra;
using TicketManager.Shared.Options;

namespace TicketManager.Infra.Database
{
    public class ApplicationDbContext : DbContext, IDatabaseContext
    {
        protected readonly string ConnectionString;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> dbContextOptions,
            IOptions<ConnectionStringsOptions> connectionStringsOptions
        ) : base(dbContextOptions)
        {
            ConnectionString = connectionStringsOptions.Value.DefaultConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
