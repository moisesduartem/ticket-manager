using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManager.Domain.Entities;

namespace TicketManager.Infra.Database.EntityConfiguration
{
    public sealed class TicketConfiguration : BaseEntityConfiguration<Ticket>
    {
        public new void Configure(EntityTypeBuilder<Ticket> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IsSolved);

            builder.HasOne(x => x.Author).WithMany(x => x.Tickets).HasForeignKey(x => x.AuthorId);
            builder.HasOne(x => x.Category).WithMany(x => x.Tickets).HasForeignKey(x => x.CategoryId);
            builder.HasMany(x => x.Comments).WithOne(x => x.Ticket).HasForeignKey(x => x.TicketId);
        }
    }
}
