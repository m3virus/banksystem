using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Domain.Models.Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; }
        public AccountStatusEnum AccountStatus { get; set; }
        public double AccountBalance { get; set; }
        
    }
}
