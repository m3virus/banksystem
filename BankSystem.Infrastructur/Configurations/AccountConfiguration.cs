using BankSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations
{
    public class AccountConfiguration:IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccountNumber).HasColumnType("varchar").IsRequired().HasMaxLength(16);
            builder.Property(x => x.AccountBalance).IsRequired();
            builder.Property(x => x.AccountStatus).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Account)
                .HasForeignKey<Account>(x => x.CustomerId);
        }
    }
}
