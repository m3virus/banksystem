using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.CustomException;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
