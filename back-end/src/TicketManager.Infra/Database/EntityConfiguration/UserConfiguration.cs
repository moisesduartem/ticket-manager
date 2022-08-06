using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManager.Domain.Entities;

namespace TicketManager.Infra.Database.EntityConfiguration
{
    public sealed class UserConfiguration : BaseEntityConfiguration<User>
    {
        public new void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Hash).IsRequired();
            builder.Property(x => x.Salt).IsRequired();
            builder.Property(x => x.Hash).IsRequired();

            builder.HasMany(x => x.Comments).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId);
        }
    }
}
