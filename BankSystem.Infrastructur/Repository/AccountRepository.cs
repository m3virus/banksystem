using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;

namespace BankSystem.Infrastructure.Repository
{
    public class AccountRepository:BaseRepository<Account>,IAccountRepository
    {
        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
