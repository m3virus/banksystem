using System.Globalization;
using BankSystem.Domain.Extensions;
using BankSystem.Domain.Statics;

namespace BankSystem.Domain.Models.Base
{
    public class BaseEntity
    {
        

        public Guid Id { get; private set; } = new();
        public DateTime CreatedAt { get; }
        public string PersianCreatedAt => CreatedAt.GeorgianToPersian(DateTimeFarmatStatics.DateAndHour);
        public bool IsDeleted { get; set; } = false;
    }
}
