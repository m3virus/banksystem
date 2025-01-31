using BankSystem.Application.Extensions.ToEntityExtensions;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BankSystem.Application.CQRS.CustomerService.Queries
{
    public class CustomerGetQueryHandler : IRequestHandler<CustomerGetQuery, BaseResponse<List<CustomerSearchModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerGetQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<CustomerSearchModel>>> Handle(CustomerGetQuery request, CancellationToken cancellationToken)
        {

            var query = _unitOfWork.CustomerRepository.GetQueryable(includes: x => x.Account);
                

            if (query == null)
            {
                var result = new List<CustomerSearchModel>();
                return BaseResponse.Success(result);
            }

            if (request.Id is not null && !request.Id.Equals(Guid.Empty))
            {
                query = query.Where(x => x.Id == request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.NationalCode))
            {
                query = query.Where(x => x.NationalCode == request.NationalCode);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name == request.Name);
            }

            if (!string.IsNullOrWhiteSpace(request.AccountNumber))
            {
                query = query.Where(x => x.Account.AccountNumber == request.AccountNumber);
            }

            var customers = await query.ToListAsync(cancellationToken: cancellationToken);

            var response =new List<CustomerSearchModel>();
            foreach (var customer in customers)
            {
                var transaction = _unitOfWork.BankTransactionRepository.
                    GetQueryable(x => x.DestinationAccountId == customer.AccountId || x.OriginAccountId == customer.AccountId)
                    .MaxBy(x => x.CreatedAt);

                response.Add(customer.ToSearchModel(transaction));
            }
            return BaseResponse.Success(response);
        }
    }
}
