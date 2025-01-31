using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;

namespace BankSystem.Infrastructure.IRepository
{
    public interface IAccountRepository: IBaseRepository<Account>
    {
        Task<BaseResponse> UpdateAsync(Account entity);
    }
}
