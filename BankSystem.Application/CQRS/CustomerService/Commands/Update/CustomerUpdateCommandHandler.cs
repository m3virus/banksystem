using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.Extensions.ToEntityExtensions;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Update
{
    public class CustomerUpdateCommandHandler:IRequestHandler<CustomerUpdateCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerUpdateCommandHandler> _logger;

        public CustomerUpdateCommandHandler(IUnitOfWork unitOfWork, ILogger<CustomerUpdateCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);

            if (model == null)
            {
                _logger.LogError($"Update Failed {nameof(CustomerUpdateCommandHandler)}" +
                                 $"{Error.CustomerNotFound.Message}");
                return BaseResponse.Failure(Error.CustomerNotFound);
            }

            model = model.ToCustomer(request);

            var result = await _unitOfWork.CustomerRepository.UpdateCustomerAsync(model, cancellationToken);
            if (result.IsFailure)
            {
                _logger.LogError($"Update Failed {nameof(CustomerUpdateCommandHandler)}" +
                                 $"{result.Error.Message}");
            }
            return result;
        }
    }
}
