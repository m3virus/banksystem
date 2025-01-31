using System.Security.Cryptography.X509Certificates;
using BankSystem.Application.Extensions.ToEntityExtensions;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using BankSystem.Infrastructure.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace BankSystem.Application.CQRS.CustomerService.Queries
{
    public class CustomerGetQueryHandler : IRequestHandler<CustomerGetQuery, BaseResponse<List<CustomerSearchModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private BankInfoOption Option;

        public CustomerGetQueryHandler(IUnitOfWork unitOfWork, IOptions<BankInfoOption> option)
        {
            _unitOfWork = unitOfWork;
            Option = option.Value;
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

            var customers = await query.ToListAsync(cancellationToken);
            try
            {
                var response = new List<CustomerSearchModel>();
                foreach (var customer in customers)
                {
                    var transaction = await _unitOfWork.BankTransactionRepository.GetQueryable()
                        .Where(x => x.DestinationAccountId != new Guid(Option.AccountId)
                                    && x.OriginAccountId != new Guid(Option.AccountId))
                        //.OrderByDescending(x => x.CreatedAt)
                        .FirstOrDefaultAsync(cancellationToken);
                    var mod = customer.ToSearchModel(transaction);
                    response.Add(mod);
                }
                return BaseResponse.Success(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

    }
}
