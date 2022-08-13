using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.Api.Infra.Database.EntityConfiguration
{
    internal sealed class CommentConfiguration : BaseEntityConfiguration<Comment>
    {
        public new void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Content).IsRequired().HasMaxLength(180);
            builder.HasOne(x => x.Author).WithMany(x => x.Comments).HasForeignKey(x => x.AuthorId);
            builder.HasOne(x => x.Ticket).WithMany(x => x.Comments).HasForeignKey(x => x.TicketId);
        }
    }
}
