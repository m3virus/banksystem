using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.Extensions.ToEntityExtensions;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Update
{
    public class CustomerUpdateCommandHandler:IRequestHandler<CustomerUpdateCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);

            if (model == null)
            {
                return BaseResponse.Failure(Error.CustomerNotFound);
            }

            model = model.ToCustomer(request);

            var result = await _unitOfWork.CustomerRepository.UpdateCustomerAsync(model, cancellationToken);
            return result;
        }
    }
}
