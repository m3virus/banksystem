﻿using BankSystem.Application.Extensions.ToEntityExtensions;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankSystem.Application.CQRS.CustomerService.Commands.Create
{
    public class CustomerCreateCommandHandler:IRequestHandler<CustomerCreateCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerCreateCommandHandler> _logger;

        public CustomerCreateCommandHandler(IUnitOfWork unitOfWork, ILogger<CustomerCreateCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResponse<string>> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var model = request.ToCustomer();

            var result = await _unitOfWork.CustomerRepository.AddCustomerWithAccountAsync(model, cancellationToken);
            if (result.IsFailure)
            {
                _logger.LogError($"Create Failed {nameof(CustomerCreateCommandHandler)}" +
                                 $"{result.Error.Message}");
            }
            return result;
        }
    }
}
