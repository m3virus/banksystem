using BankSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankSystem.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChangeTracking> ChangeTrackings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
