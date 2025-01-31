using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;

namespace BankSystem.Infrastructure.IRepository
{
    public interface ICustomerRepository:IBaseRepository<Customer>
    {
        Task<BaseResponse<string>> AddCustomerWithAccountAsync(Customer customer, CancellationToken cancellation);
        Task<BaseResponse> DeleteCustomerWithAccountAsync(Customer customer, CancellationToken cancellation);
        Task<BaseResponse> UpdateCustomerAsync(Customer customer, CancellationToken cancellation);

    }
}
