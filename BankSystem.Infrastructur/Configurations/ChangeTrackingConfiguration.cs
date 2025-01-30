using BankSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations
{
    public class ChangeTrackingConfiguration: IEntityTypeConfiguration<ChangeTracking>
    {
        public void Configure(EntityTypeBuilder<ChangeTracking> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(x => x.Entity).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            builder.Property(x => x.Status).IsRequired().HasColumnType("varchar").HasMaxLength(10);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ChangeTrackings)
                .HasForeignKey(x => x.UserId);
        }
    }
}
