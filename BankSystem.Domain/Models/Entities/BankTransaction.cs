using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Domain.Models.Entities
{
    public class BankTransaction : BaseEntity
    {
        #region Properties
        public double TransactionValue { get; set; }
        public BankTransactionEnum TransactionEnum { get; set; }
        public string TransactionNumber { get; set; }

        #endregion

        #region Relations

        public Guid? OriginAccountId { get; set; }
        public Guid? DestinationAccountId { get; set; }

        public virtual Account? OriginAccount { get; set; }
        public virtual Account? DestinationAccount { get; set;}

        #endregion
    }
}
