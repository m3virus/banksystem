using BankSystem.Domain.Extensions;
using BankSystem.Domain.Statics;

namespace BankSystem.Domain.Models.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public string PersianCreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
