using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Application.Models.CustomerModel
{
    public class BankTransactionSearchModel
    {
        public double TransactionValue { get; set; }
        public string TransactionType { get; set; }
        public string TransactionNumber { get; set; }

        public Guid? OriginAccountId { get; set; }
        public Guid? DestinationAccountId { get; set; }
    }
}
