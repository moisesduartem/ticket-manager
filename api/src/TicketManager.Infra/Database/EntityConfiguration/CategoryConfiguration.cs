using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManager.Domain.Entities;

namespace TicketManager.Infra.Database.EntityConfiguration
{
    public sealed class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public new void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.HasMany(x => x.Tickets).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
