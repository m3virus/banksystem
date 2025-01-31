using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Delete
{
    public class CustomerDeleteCommandHandler:IRequestHandler<CustomerDeleteCommand,BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            var model = _unitOfWork.CustomerRepository.GetQueryable(x => x.Id == request.Id,includes: x => x.Account)
                .FirstOrDefault();
            if (model == null)
            {
                BaseResponse.Failure(Error.CustomerNotFound);
            }

            var result = await _unitOfWork.CustomerRepository.DeleteCustomerWithAccountAsync(model!, cancellationToken);
            return result;
        }
    }
}
