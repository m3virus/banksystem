using BankSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations
{
    public class BankTransactionConfiguration:IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransactionNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.TransactionEnum).IsRequired();
            builder.Property(x => x.TransactionValue).IsRequired();

            builder.HasOne(x => x.DestinationAccount)
                .WithMany(x => x.TransactionsAsDestination)
                .HasForeignKey(x => x.DestinationAccountId);

            builder.HasOne(x => x.OriginAccount)
                .WithMany(x => x.TransactionsAsOrigin)
                .HasForeignKey(x => x.OriginAccountId);


        }
    }
}
