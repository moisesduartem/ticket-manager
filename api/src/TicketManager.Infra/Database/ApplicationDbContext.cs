using Microsoft.EntityFrameworkCore;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Enums;

namespace TicketManager.Infra.Database
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly string ConnectionString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(
                    id: 1, 
                    name: "John L.", 
                    email: "johnl@email.com", 
                    hash: "$2a$12$dNMivl8uiVUbJOanAbvggOThv0Psr6oaUEAf7dtgTYFB3x.PCfmr.",
                    salt: "1d8cf748c6156545d92237c8ef115f25",
                    role: UserRole.Admin
               ),
                new User(
                    id: 2, 
                    name: "George H.", 
                    email: "georgeh@email.com", 
                    hash: "$2a$12$mc3HQ1zD.C8AAr/Lf2I29.XB.MJz6n5FLEXUOllaXPt0PdgveXU7C",
                    salt: "ccea7ca67997fc7437b1c19a482143a7",
                    role: UserRole.Regular
               )
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
