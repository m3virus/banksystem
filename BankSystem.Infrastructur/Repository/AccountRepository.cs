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

        public async Task<BaseResponse> UpdateAsync(Account entity)
        {
            await using var transaction =  await DbContext.Database.BeginTransactionAsync();
            var track = new List<ChangeTracking>();
            try
            {
                DbContext.Accounts.Update(entity);
                var customerTrack = ChangeTrackingService.CreateChangeTracking(nameof(Account),
                    EntityState.Modified.ToString());

                track.Add(customerTrack);

                DbContext.ChangeTrackings.AddRange(track);

                var result = await DbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return BaseResponse.Success();
            }
            catch (Exception e)
            {
                
                await transaction.RollbackAsync();
                return BaseResponse.Failure<int>(Error.UpdateFailed);
            }
        }
    }
}
