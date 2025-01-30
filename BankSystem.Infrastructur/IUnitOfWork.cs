using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Infrastructure.IRepository;

namespace BankSystem.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        public IAccountRepository AccountRepository { get; }
        public IBankTransactionRepository BankTransactionRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IChangeTrackingRepository ChangeTrackingRepository { get; }
        public IUserRepository UserRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
