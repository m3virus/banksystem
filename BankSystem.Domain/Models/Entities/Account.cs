using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Domain.Models.Entities
{
    public class Account : BaseEntity
    {
        #region Properties

        public string AccountNumber { get; set; }
        public AccountStatusEnum AccountStatus { get; set; }
        public double AccountBalance { get; set; }

        #endregion

        #region Relations

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<BankTransaction> BankTransactions { get; set; }

        #endregion
    }
}
