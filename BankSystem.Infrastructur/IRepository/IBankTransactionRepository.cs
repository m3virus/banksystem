using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;

namespace BankSystem.Infrastructure.IRepository
{
    public interface IBankTransactionRepository:IBaseRepository<BankTransaction>
    {
        BaseResponse AddWithAccountBalance(IEnumerable<BankTransaction> entities, IEnumerable<Account> accounts);
    }
}
