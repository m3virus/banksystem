using BankSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace BankSystem.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        protected AppDbContext(IHttpContextAccessor contextAccessor, DbSet<User> users, DbSet<ChangeTracking> changeTrackings, DbSet<Customer> customers, DbSet<BankTransaction> bankTransactions, DbSet<Account> accounts)
        {
            _contextAccessor = contextAccessor;
            Users = users;
            ChangeTrackings = changeTrackings;
            Customers = customers;
            BankTransactions = bankTransactions;
            Accounts = accounts;
        }

        public AppDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor, DbSet<User> users, DbSet<ChangeTracking> changeTrackings, DbSet<Customer> customers, DbSet<BankTransaction> bankTransactions, DbSet<Account> accounts) : base(options)
        {
            _contextAccessor = contextAccessor;
            Users = users;
            ChangeTrackings = changeTrackings;
            Customers = customers;
            BankTransactions = bankTransactions;
            Accounts = accounts;
        }

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

        //public override int SaveChanges()
        //{

        //    var changeLogs = new List<ChangeTracking>();

        //    // Get current user
        //    var username = _contextAccessor.HttpContext?.User?.Identity?.Name
        //                   ?? "UnknownUser";
        //    var userId = Users.FirstOrDefault(x => x.UserName == username)?.Id;

        //    if (userId == null)
        //    {
        //        throw new UserInvalidException("Your User Can't Identified");
        //    }

        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        // Don't track the ChangeTracking table itself
        //        if (entry.Entity is ChangeTracking)
        //            continue;

        //        var changeType = entry.State
        //            switch
        //        {
        //            EntityState.Added => "Added",
        //            EntityState.Modified => "Modified",
        //            EntityState.Deleted => "Deleted",
        //            _ => null
        //        };

        //        if (changeType != null)
        //        {

        //            var changeLog = new ChangeTracking
        //            {
        //                Entity = entry.Entity.GetType().Name,
        //                Status = changeType,
        //                CreatedAt = DateTime.Now
        //            };

        //            changeLogs.Add(changeLog);
        //        }
        //    }

        //    // Add logs to database
        //    ChangeTrackings.AddRange(changeLogs);

        //    // Save all changes, including logs
        //    return base.SaveChanges();


        //}

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    var changeLogs = new List<ChangeTracking>();

        //    // Get current user
        //    var username = _contextAccessor.HttpContext?.User?.Identity?.Name
        //                   ?? "UnknownUser";
        //    var userId = (await Users.FirstOrDefaultAsync(x => x.UserName == username,cancellationToken))?.Id;

        //    if (userId == null)
        //    {
        //        throw new UserInvalidException("Your User Can't Identified");
        //    }

        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        // Don't track the ChangeTracking table itself
        //        if (entry.Entity is ChangeTracking)
        //            continue;

        //        var changeType = entry.State
        //            switch
        //            {
        //                EntityState.Added => "Added",
        //                EntityState.Modified => "Modified",
        //                EntityState.Deleted => "Deleted",
        //                _ => null
        //            };

        //        if (changeType != null)
        //        {

        //            var changeLog = new ChangeTracking
        //            {
        //                Entity = entry.Entity.GetType().Name,
        //                Status = changeType,
        //                CreatedAt = DateTime.Now,
        //                UserId = (Guid)userId
        //            };

        //            changeLogs.Add(changeLog);
        //        }
        //    }

        //    // Add logs to database
        //    await ChangeTrackings.AddRangeAsync(changeLogs, cancellationToken);

        //    // Save all changes, including logs
        //    return await base.SaveChangesAsync(cancellationToken);
        //}
    }
}
