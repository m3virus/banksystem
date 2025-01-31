using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.Models.CustomerModel;
using BankSystem.Domain.Extensions;
using BankSystem.Domain.Models.Entities;

namespace BankSystem.Application.Extensions.ToEntityExtensions
{
    public static class ToBankTransactionExtensions
    {
        public static BankTransactionSearchModel ToSearchModel(this BankTransaction model)
        {
            var result = new BankTransactionSearchModel()
            {
                DestinationAccountId = model.DestinationAccountId,
                OriginAccountId = model.OriginAccountId,
                TransactionNumber = model.TransactionNumber,
                TransactionType = model.TransactionEnum.ToEnumTitle(),
                TransactionValue = model.TransactionValue
            };

            return result;
        }
    }
}
