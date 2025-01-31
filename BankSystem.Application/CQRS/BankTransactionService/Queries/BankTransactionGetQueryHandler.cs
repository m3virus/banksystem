using BankSystem.Application.Extensions.ToEntityExtensions;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Application.CQRS.BankTransactionService.Queries
{
    public class BankTransactionGetQueryHandler : IRequestHandler<BankTransactionGetQuery, BaseResponse<List<BankTransactionSearchModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankTransactionGetQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<BankTransactionSearchModel>>> Handle(BankTransactionGetQuery request, CancellationToken cancellationToken)
        {
            var accountId = (await _unitOfWork.AccountRepository.GetAsync(x => x.AccountNumber == request.AccountNumber))?.Id;
            if (accountId is null)
            {
                return BaseResponse.Success(new List<BankTransactionSearchModel>());
            }
            var query = _unitOfWork.BankTransactionRepository
                .GetQueryable(x => x.DestinationAccountId == accountId || x.OriginAccountId == accountId);


            if (query == null)
            {
                var result = new List<BankTransactionSearchModel>();
                return BaseResponse.Success(result);
            }

            if (request.BankTransaction.Any())
            {
                query = query.Where(x => request.BankTransaction.Contains(x.TransactionEnum));
            }

            if (!request.StartingDate.Equals(DateTime.MinValue) && request.StartingDate is not null)
            {
                query = query.Where(x => x.CreatedAt >= request.StartingDate);
            }

            if (!request.EndDate.Equals(DateTime.MinValue) && request.EndDate is not null)
            {
                query = query.Where(x => x.CreatedAt <= request.EndDate);
            }
            if (!request.StartingAmount.Equals(0.0) && request.StartingAmount is not null)
            {
                query = query.Where(x => x.TransactionValue >= request.StartingAmount);
            }

            if (!request.StartingAmount.Equals(0.0) && request.EndingAmount is not null)
            {
                query = query.Where(x => x.TransactionValue <= request.EndingAmount);
            }
            query = query.OrderByDescending(x => x.CreatedAt);

            if (request.Take != 0 && request.Take is not null)
            {
                query = query.Take((int)request.Take);
            }

            var customer = await query.ToListAsync(cancellationToken);
            
            if (request.page != 0 && request.page is not null && request.Weight != 0 && request.Weight is not null)
            {
                var page = Convert.ToInt32(request.page) - 1;
                var weight = Convert.ToInt32(request.Weight);
                var skip = page * weight;
                customer = customer.Skip(skip).Take(weight).ToList();
            }

            var response = customer.Select(x => x.ToSearchModel()).ToList();
            return BaseResponse.Success(response);
        }
    }
}
