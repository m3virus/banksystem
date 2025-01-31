using BankSystem.Domain.Extensions;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Domain.Models.Enums;
using BankSystem.Domain.Statics;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace BankSystem.Infrastructure.Repository
{
    public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
    {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BaseResponse<string>> AddCustomerWithAccountAsync(Customer customer, CancellationToken cancellation)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellation);
            var track = new List<ChangeTracking>();
            try
            {
                await DbContext.Customers.AddAsync(customer, cancellation);
                var customerTrack = ChangeTrackingService.CreateChangeTracking(nameof(Customer),
                    EntityState.Added.ToString());

                track.Add(customerTrack);

                var account = new Account
                {
                    CustomerId = customer.Id,
                    AccountBalance = 0,
                    AccountStatus = AccountStatusEnum.Inactive,
                    AccountNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                };
               

                await DbContext.Accounts.AddAsync(account, cancellation);
                var accountTrack = ChangeTrackingService.CreateChangeTracking(nameof(Account),
                    EntityState.Added.ToString());
                track.Add(accountTrack);

                await DbContext.ChangeTrackings.AddRangeAsync(track,cancellation);

                await DbContext.SaveChangesAsync(cancellation);

                await transaction.CommitAsync(cancellation);

                return BaseResponse.Success(account.AccountNumber);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellation);
                return BaseResponse.Failure<string>(Error.CreateFailed);
            }
        }
        public async Task<BaseResponse> DeleteCustomerWithAccountAsync(Customer customer, CancellationToken cancellation)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellation);
            var track = new List<ChangeTracking>();
            try
            {
                DbContext.Customers.Remove(customer);
                
                var customerTrack = ChangeTrackingService.CreateChangeTracking(nameof(Customer),
                    EntityState.Deleted.ToString());

                track.Add(customerTrack);

                DbContext.Accounts.Remove(customer.Account);
                

                var accountTrack = ChangeTrackingService.CreateChangeTracking(nameof(Account),
                    EntityState.Deleted.ToString());
                track.Add(accountTrack);

                await DbContext.ChangeTrackings.AddRangeAsync(track, cancellation);

                await DbContext.SaveChangesAsync(cancellation);

                await transaction.CommitAsync(cancellation);

                return BaseResponse.Success();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellation);
                return BaseResponse.Failure(Error.DeleteFailed);
            }
        }

        public async Task<BaseResponse> UpdateCustomerAsync(Customer customer, CancellationToken cancellation)
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellation);
            var track = new List<ChangeTracking>();
            try
            {
                DbContext.Customers.Update(customer);
                var customerTrack = ChangeTrackingService.CreateChangeTracking(nameof(Customer),
                    EntityState.Modified.ToString());

                track.Add(customerTrack);

                await DbContext.ChangeTrackings.AddRangeAsync(track, cancellation);

                await DbContext.SaveChangesAsync(cancellation);

                await transaction.CommitAsync(cancellation);

                return BaseResponse.Success();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellation);
                return BaseResponse.Failure(Error.UpdateFailed);
            }
        }
    }
}
