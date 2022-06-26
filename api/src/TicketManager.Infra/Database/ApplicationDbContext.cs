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
                    hash: "$2a$12$A77iNnPPdii48qrXVAtEFuiODMov/jw7pDshr0WGFZ9foy5MO9Zbu",
                    salt: "1d8cf748c6156545d92237c8ef115f25",
                    role: UserRole.Admin
               ),
                new User(
                    id: 2, 
                    name: "George H.", 
                    email: "georgeh@email.com",
                    hash: "$2a$12$KvEXEGNvgV9xyOZFxgwqfuckZBwgtAuGlSaLmWxWGPrLe7q59.POu",
                    salt: "ccea7ca67997fc7437b1c19a482143a7",
                    role: UserRole.Regular
               )
            );
            
            modelBuilder.Entity<Category>().HasData(
                new Category(
                    id: 1, 
                    name: "Notebook"
               ),
                new Category(
                    id: 2, 
                    name: "Network"
               ),new Category(
                    id: 3, 
                    name: "PC"
               ),new Category(
                    id: 4, 
                    name: "Printer"
               ),new Category(
                    id: 5, 
                    name: "Other"
               )
            );
            
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket(
                    id: 1, 
                    title: "My notebook is on fire!",
                    description: "I don't what happened, but it started to flame from nothing",
                    authorId: 1,
                    categoryId: 1,
                    isSolved: false
               ),
                new Ticket(
                    id: 2, 
                    title: "I'm without internet",
                    description: "",
                    authorId: 1,
                    categoryId: 2,
                    isSolved: false
               ),
                new Ticket(
                    id: 3,
                    title: "My printer isn't working",
                    description: "",
                    authorId: 2,
                    categoryId: 4,
                    isSolved: false
               )
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
