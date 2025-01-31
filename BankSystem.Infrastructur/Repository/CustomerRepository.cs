﻿using BankSystem.Domain.Extensions;
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
                //todo: User Id should change
                var customerTrack = ChangeTrackingService.CreateChangeTracking(nameof(Customer),
                    EntityState.Added.ToString(), Guid.NewGuid());

                track.Add(customerTrack);

                var account = new Account
                {
                    CustomerId = customer.Id,
                    AccountBalance = 0,
                    AccountStatus = AccountStatusEnum.Inactive,
                    AccountNumber = DateTime.Now.GeorgianToPersian(DateTimeFarmatStatics.SpecifiedForGeneration)
                };
                //todo: User Id should change

                await DbContext.Accounts.AddAsync(account, cancellation);
                var accountTrack = ChangeTrackingService.CreateChangeTracking(nameof(Account),
                    EntityState.Added.ToString(), Guid.NewGuid());
                track.Add(accountTrack);

                await DbContext.ChangeTrackings.AddRangeAsync(track,cancellation);

                await DbContext.SaveChangesAsync(cancellation);

                await transaction.CommitAsync(cancellation);

                return BaseResponse.Success(account.AccountNumber);
            }
            catch (Exception)
            {
                return BaseResponse.Failure<string>(Error.CreateFailed);
            }
        }

    }
}
