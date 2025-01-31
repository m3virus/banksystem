using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repository
{
    public class AccountRepository:BaseRepository<Account>,IAccountRepository
    {
        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void Update(Account entity)
        { 
            using var transaction =  DbContext.Database.BeginTransaction();
            var track = new List<ChangeTracking>();
            try
            {
                DbContext.Accounts.Update(entity);
                var customerTrack = ChangeTrackingService.CreateChangeTracking(nameof(Account),
                    EntityState.Modified.ToString());

                track.Add(customerTrack);

                DbContext.ChangeTrackings.AddRange(track);

                DbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(nameof(Account), e);
            }
        }
    }
}
