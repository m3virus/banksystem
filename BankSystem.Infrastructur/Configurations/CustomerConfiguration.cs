using BankSystem.Domain.Models.Entities;
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
            builder.Property(x => x.NationalCode).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.PostCode).IsRequired();
            
        }
    }
}
