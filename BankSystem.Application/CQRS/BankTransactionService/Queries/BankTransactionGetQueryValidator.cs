using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BankSystem.Application.CQRS.BankTransactionService.Queries
{
    public class BankTransactionGetQueryValidator:AbstractValidator<BankTransactionGetQuery>
    {
        public BankTransactionGetQueryValidator()
        {
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
        }
    }
}
