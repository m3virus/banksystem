using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Domain.Models.Entities
{
    public class BankTransaction : BaseEntity
    {
        public double TransactionValue { get; set; }

        public BankTransactionEnum TransactionEnum { get; set; }
    }
}
