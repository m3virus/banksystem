using BankSystem.Domain.Extensions;
using BankSystem.Domain.Statics;

namespace BankSystem.Domain.Models.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt => DateTime.Now;
        public string PersianCreatedAt => CreatedAt.GeorgianToPersian(DateTimeFormatStatics.DateAndHour);
        public bool IsDeleted { get; set; } = false;
    }
}
