using BankSystem.Domain.Models.Entities;
using BankSystem.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configurations
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.NationalCode).IsUnique();

            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.NationalCode).IsRequired().HasMaxLength(14);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(12);
            builder.Property(x => x.PostCode).IsRequired().HasMaxLength(11);

            builder.HasData(BankInfo());

        }

        private Customer BankInfo()
        {
            var result = new Customer
            {
                Id = new Guid("76131e9f-6183-41ad-b3a3-9d6cdccc468d"),
                BirthDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                NationalCode = "1234567890",
                PostCode = "1234567899",
                IsDeleted = false,
                Address = "Iran-Tehran",
                Name = "Bank-Mohaymen",
                PhoneNumber = "09123456789",
                UserType = UserTypeEnum.Legal,
                AccountId = new Guid("debd3920-aadb-4d07-9b19-1f9647823a46"),
            };

            return result;
        }
    }
}
