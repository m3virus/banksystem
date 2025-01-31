using BankSystem.Domain.Extensions;
using BankSystem.Domain.Models.Entities;
using BankSystem.Domain.Models.Enums;
using BankSystem.Domain.Statics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
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

            builder.HasData(BankAccount());
        }

        private Account BankAccount()
        {
            return new Account
            {
                Id = new Guid("debd3920-aadb-4d07-9b19-1f9647823a46"),
                CreatedAt = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                PersianCreatedAt = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc).GeorgianToPersian(DateTimeFormatStatics.DateAndHour),
                AccountBalance = 0.0,
                AccountNumber = "11111111111111",
                AccountStatus = AccountStatusEnum.Active,
                IsDeleted = false,
                CustomerId = new Guid("76131e9f-6183-41ad-b3a3-9d6cdccc468d") // FK Reference
            };
        }
    }
}
