using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Enums;
using FluentValidation;

namespace BankSystem.Application.CQRS.BankTransactionService.Commands.Create
{
    public class BankTransactionCreateCommandValidator : AbstractValidator<BankTransactionCreateCommand>
    {
        public BankTransactionCreateCommandValidator()
        {
            RuleFor(x => x.TransactionEnum).NotNull();

            RuleFor(x => x.TransactionValue).NotNull().Must((x, y) =>
                x.TransactionValue > 10000);

            RuleFor(x => x.TransactionEnum == BankTransactionEnum.Deposit).Must((x, y)
                => x.DestinationAccountId is not null && x.OriginAccountId is null);
            RuleFor(x => x.TransactionEnum == BankTransactionEnum.Transmission).Must((x, y)
                => x.DestinationAccountId is not null && x.OriginAccountId is not null);
            RuleFor(x => x.TransactionEnum == BankTransactionEnum.Withdrawal).Must((x, y)
                => x.DestinationAccountId is null && x.OriginAccountId is not null);


        }
    }
}
