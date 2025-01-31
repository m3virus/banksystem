using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;
using Microsoft.Identity.Client;

namespace BankSystem.Application.CQRS.AccountService.Commands.Update
{
    public class AccountUpdateCommandHandler:IRequestHandler<AccountUpdateCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(AccountUpdateCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.AccountRepository.GetByIdAsync(request.Id);

            if (model is null)
            {
                return BaseResponse.Failure(Error.AccountNotFound);
            }

            model.AccountStatus = request.status;
            try
            {
                _unitOfWork.AccountRepository.Update(model);
                return BaseResponse.Success();
            }
            catch (Exception e)
            {
                return BaseResponse.Failure(Error.UpdateFailed);
            }
            
        }
    }
}
