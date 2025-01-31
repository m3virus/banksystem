using BankSystem.Domain.Extensions;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Domain.Models.Enums;
using BankSystem.Domain.Statics;
using BankSystem.Infrastructure;
using BankSystem.Infrastructure.Extensions;
using BankSystem.Infrastructure.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace BankSystem.Application.CQRS.BankTransactionService.Commands.Create
{
    public class BankTransactionCreateCommandHandler : IRequestHandler<BankTransactionCreateCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private BankInfoOption Options;

        public BankTransactionCreateCommandHandler(IUnitOfWork unitOfWork, IOptions<BankInfoOption> options)
        {
            _unitOfWork = unitOfWork;
            Options = options.Value;
        }

        public async Task<BaseResponse> Handle(BankTransactionCreateCommand request, CancellationToken cancellationToken)
        {
            
            lock (request)
            {
                var accountList = new List<Account>();
                if (request.TransactionEnum == BankTransactionEnum.Deposit)
                {
                    var transactions = CreateDeposit(request);
                    var destinationAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == request.DestinationAccountId).FirstOrDefault();
                    var bankAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == new Guid(Options.AccountId)).FirstOrDefault();

                    if (destinationAccount is null)
                    {
                        return BaseResponse.Failure(Error.NoDestinatinoAccountFound);
                    }

                    if (destinationAccount.AccountStatus != AccountStatusEnum.Active)
                    {
                        return BaseResponse.Failure(Error.DestinationAccountIsNotActive);
                    }
                    if (bankAccount is null)
                    {
                        return BaseResponse.Failure(Error.BankAccountNotFound);
                    }

                    destinationAccount.AccountBalance += request.TransactionValue - Convert.ToDouble(Options.BankTax);
                    bankAccount.AccountBalance += Convert.ToDouble(Options.BankTax);

                    accountList.Add(destinationAccount);
                    accountList.Add(bankAccount);

                    var result = _unitOfWork.BankTransactionRepository.AddWithAccountBalance(transactions, accountList);
                    return result;
                }

                if (request.TransactionEnum == BankTransactionEnum.Transmission)
                {
                    if (request.OriginAccountId == request.DestinationAccountId)
                    {
                        return BaseResponse.Failure(Error.SameAccountFound);
                    }

                    var transactions = CreateTransmission(request);
                    var destinationAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == request.DestinationAccountId).FirstOrDefault();
                    var bankAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == new Guid(Options.AccountId)).FirstOrDefault();
                    var originAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == request.OriginAccountId).FirstOrDefault();

                    if (destinationAccount is null)
                    {
                        return BaseResponse.Failure(Error.NoDestinatinoAccountFound);
                    }
                    if (destinationAccount.AccountStatus != AccountStatusEnum.Active)
                    {
                        return BaseResponse.Failure(Error.DestinationAccountIsNotActive);
                    }
                    if (originAccount is null)
                    {
                        return BaseResponse.Failure(Error.NoDestinatinoAccountFound);
                    }
                    if (originAccount.AccountStatus != AccountStatusEnum.Active)
                    {
                        return BaseResponse.Failure(Error.DestinationAccountIsNotActive);
                    }
                    if (bankAccount is null || bankAccount.AccountStatus != AccountStatusEnum.Active)
                    {
                        return BaseResponse.Failure(Error.BankAccountNotFound);
                    }

                    var balance = _unitOfWork.AccountRepository.GetQueryable(x => x.Id == request.OriginAccountId).FirstOrDefault()?.AccountBalance;
                    if (balance < transactions.Sum(x => x.TransactionValue))
                    {
                        return BaseResponse.Failure(Error.OriginAccountDoesntHaveEnoughMoney);
                    }

                    destinationAccount.AccountBalance += request.TransactionValue - Convert.ToDouble(Options.BankTax);
                    bankAccount.AccountBalance += Convert.ToDouble(Options.BankTax);
                    originAccount.AccountBalance -= request.TransactionValue;

                    accountList.Add(destinationAccount);
                    accountList.Add(bankAccount);
                    accountList.Add(originAccount);

                    
                    var result = _unitOfWork.BankTransactionRepository.AddWithAccountBalance(transactions, accountList);
                    return result;
                }

                if (request.TransactionEnum == BankTransactionEnum.Withdrawal)
                {
                    var transactions = CreateWithdrawal(request);
                    var bankAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == new Guid(Options.AccountId)).FirstOrDefault();
                    var originAccount =
                        _unitOfWork.AccountRepository.GetQueryable(x => x.Id == request.OriginAccountId).FirstOrDefault();

                    if (originAccount is null)
                    {
                        return BaseResponse.Failure(Error.NoDestinatinoAccountFound);
                    }
                    if (originAccount.AccountStatus != AccountStatusEnum.Active)
                    {
                        return BaseResponse.Failure(Error.DestinationAccountIsNotActive);
                    }
                    if (bankAccount is null || bankAccount.AccountStatus != AccountStatusEnum.Active)
                    {
                        return BaseResponse.Failure(Error.BankAccountNotFound);
                    }

                    var balance = _unitOfWork.AccountRepository.GetQueryable(x => x.Id == request.OriginAccountId).FirstOrDefault()?.AccountBalance;
                    if (balance < transactions.Sum(x => x.TransactionValue))
                    {
                        return BaseResponse.Failure(Error.OriginAccountDoesntHaveEnoughMoney);
                    }

                    bankAccount.AccountBalance += Convert.ToDouble(Options.BankTax);
                    originAccount.AccountBalance -= request.TransactionValue;

                    accountList.Add(bankAccount);
                    accountList.Add(originAccount);
                    var result = _unitOfWork.BankTransactionRepository.AddWithAccountBalance(transactions, accountList);
                    return result;
                }
            }
            return BaseResponse.Failure(Error.CreateFailed);
        }

        private List<BankTransaction> CreateDeposit(BankTransactionCreateCommand request)
        {
            var model = new List<BankTransaction>
            {
                new()
                {
                    DestinationAccountId = request.DestinationAccountId,
                    TransactionEnum = BankTransactionEnum.Deposit,
                    TransactionNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                        .GenerateTwentyDigitString(),
                    TransactionValue = request.TransactionValue - double.Parse(Options.BankTax),

                },
                new()
                {
                    DestinationAccountId = request.DestinationAccountId,
                    OriginAccountId = new Guid(Options.AccountId),
                    TransactionEnum = BankTransactionEnum.Transmission,
                    TransactionNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                        .GenerateTwentyDigitString(),
                    TransactionValue = request.TransactionValue - double.Parse(Options.BankTax),
                }
            };
            return model;
        }
        private List<BankTransaction> CreateTransmission(BankTransactionCreateCommand request)
        {
            var model = new List<BankTransaction>
            {
                new()
                {
                    DestinationAccountId = request.DestinationAccountId,
                    OriginAccountId = request.OriginAccountId,
                    TransactionEnum = BankTransactionEnum.Transmission,
                    TransactionNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                        .GenerateTwentyDigitString(),
                    TransactionValue = request.TransactionValue - double.Parse(Options.BankTax),

                },
                new()
                {
                    DestinationAccountId = request.DestinationAccountId,
                    OriginAccountId = new Guid(Options.AccountId),
                    TransactionEnum = BankTransactionEnum.Transmission,
                    TransactionNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                        .GenerateTwentyDigitString(),
                    TransactionValue = request.TransactionValue - double.Parse(Options.BankTax),
                }
            };
            return model;
        }
        private List<BankTransaction> CreateWithdrawal(BankTransactionCreateCommand request)
        {
            var model = new List<BankTransaction>
            {
                new()
                {
                    OriginAccountId = request.OriginAccountId,
                    TransactionEnum = BankTransactionEnum.Withdrawal,
                    TransactionNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                        .GenerateTwentyDigitString(),
                    TransactionValue = request.TransactionValue - double.Parse(Options.BankTax),

                },
                new()
                {
                    DestinationAccountId = request.DestinationAccountId,
                    OriginAccountId = new Guid(Options.AccountId),
                    TransactionEnum = BankTransactionEnum.Transmission,
                    TransactionNumber = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration)
                        .GenerateTwentyDigitString(),
                    TransactionValue = request.TransactionValue - double.Parse(Options.BankTax),
                }
            };
            return model;
        }
    }
}
