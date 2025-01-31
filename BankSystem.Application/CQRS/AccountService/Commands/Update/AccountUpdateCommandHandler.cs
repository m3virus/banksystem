using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BankSystem.Application.CQRS.AccountService.Commands.Update
{
    public class AccountUpdateCommandHandler : IRequestHandler<AccountUpdateCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountUpdateCommandHandler> _logger;

        public AccountUpdateCommandHandler(IUnitOfWork unitOfWork, ILogger<AccountUpdateCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                var result = await _unitOfWork.AccountRepository.UpdateAsync(model);
                if (result.IsSuccess)
                {
                    return BaseResponse.Success();
                }

                _logger.LogError($"Update Failed {nameof(AccountUpdateCommandHandler)}");
                return BaseResponse.Failure(Error.UpdateFailed);

            }
            catch (Exception e)
            {
                _logger.LogCritical($"Update Failed {nameof(AccountUpdateCommandHandler)}" +
                                    $"Exception: {e.Message}" +
                                    $"inner:{e.InnerException?.Message}");
                return BaseResponse.Failure(Error.UpdateFailed);
            }

        }
    }
}
