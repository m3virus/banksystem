using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.CustomException;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure
{
    public class UnitOfWork(AppDbContext context, IHttpContextAccessor contextAccessor) : IUnitOfWork
    {
        private bool _disposed;
        private IAccountRepository _accountRepository;
        private IBankTransactionRepository _bankTransactionRepository;
        private ICustomerRepository _customerRepository;
        private IChangeTrackingRepository _changeTrackingRepository;
        private IUserRepository _userRepository;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // release unmanaged memory

                if (disposing)
                {
                    // release disposable objects

                    context.Dispose();
                }
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public IAccountRepository AccountRepository => _accountRepository??= new AccountRepository(context);

        public IBankTransactionRepository BankTransactionRepository => _bankTransactionRepository ??= new BankTransactionRepository(context);

        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(context);
        public IChangeTrackingRepository ChangeTrackingRepository => _changeTrackingRepository ??= new ChangeTrackingRepository(context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(context);

        public int SaveChanges()
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var changeLogs = new List<ChangeTracking>();

                // Get current user
                var userName = contextAccessor.HttpContext?.User?.Identity?.Name;
                var userId = context.Users.FirstOrDefault(x => x.UserName == userName)?.Id;

                if (userId == null)
                {
                    throw new UserInvalidException("Your User Can't Identified");
                }

                foreach (var entry in context.ChangeTracker.Entries())
                {
                    // Don't track the ChangeTracking table itself
                    if (entry.Entity is ChangeTracking)
                        continue;

                    var changeType = entry.State
                        switch
                    {
                        EntityState.Added => "Added",
                        EntityState.Modified => "Modified",
                        EntityState.Deleted => "Deleted",
                        _ => null
                    };

                    if (changeType != null)
                    {

                        var changeLog = new ChangeTracking
                        {
                            Entity = entry.Entity.GetType().Name,
                            Status = changeType,
                            CreatedAt = DateTime.Now,
                            UserId = (Guid)userId
                        };

                        changeLogs.Add(changeLog);
                    }
                }

                // Add logs to database
                ChangeTrackingRepository.Add(changeLogs);

                // Save changes
                int result = context.SaveChanges();

                // Commit transaction if successful
                transaction.Commit(); 
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback(); // Rollback if an error occurs
                throw;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var changeLogs = new List<ChangeTracking>();

                // Get current user
                var userName = contextAccessor.HttpContext?.User?.Identity?.Name;
                var userId = context.Users.FirstOrDefault(x => x.UserName == userName)?.Id;

                if (userId == null)
                {
                    throw new UserInvalidException("Your User Can't Identified");
                }

                foreach (var entry in context.ChangeTracker.Entries())
                {
                    // Don't track the ChangeTracking table itself
                    if (entry.Entity is ChangeTracking)
                        continue;

                    var changeType = entry.State
                        switch
                        {
                            EntityState.Added => "Added",
                            EntityState.Modified => "Modified",
                            EntityState.Deleted => "Deleted",
                            _ => null
                        };

                    if (changeType != null)
                    {

                        var changeLog = new ChangeTracking
                        {
                            Entity = entry.Entity.GetType().Name,
                            Status = changeType,
                            CreatedAt = DateTime.Now,
                            UserId = (Guid)userId
                        };

                        changeLogs.Add(changeLog);
                    }
                }

                // Add logs to database
                ChangeTrackingRepository.Add(changeLogs);

                // Save changes
                int result = await context.SaveChangesAsync(cancellationToken);

                // Commit transaction if successful
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken); // Rollback if an error occurs
                throw;
            }
        }
    }
}
