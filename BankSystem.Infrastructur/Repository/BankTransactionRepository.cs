using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repository
{
    public class BankTransactionRepository:BaseRepository<BankTransaction>,IBankTransactionRepository
    {
        public BankTransactionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public BaseResponse AddWithAccountBalance(IEnumerable<BankTransaction> entities, IEnumerable<Account> accounts)
        {
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                DbContext.BankTransactions.AddRange(entities);
                
                DbContext.Accounts.AddRange(accounts);

                var customerTrack = ChangeTrackingService.CreateChangeTracking($"{nameof(BankTransaction)}-{nameof(Account)}",
                    EntityState.Added.ToString(), Guid.NewGuid());

                DbContext.ChangeTrackings.Add(customerTrack);

                DbContext.SaveChanges();
                
                transaction.Commit();

                return BaseResponse.Success();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return BaseResponse.Failure(Error.CreateFailed);
            }
        }
    }
}
