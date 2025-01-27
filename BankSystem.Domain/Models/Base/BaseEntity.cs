using System.Globalization;

namespace BankSystem.Domain.Models.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            var faCultureInfo = new CultureInfo("fa-IR");
            faCultureInfo.DateTimeFormat.Calendar = new PersianCalendar();

            CreatedAt = DateTime.Now;
            PersianCreatedAt = CreatedAt.ToString("yyyy-M-d", faCultureInfo);
        }

        public Guid Id { get; private set; } = new();
        public DateTime CreatedAt { get; }
        public string PersianCreatedAt { get; }
        public bool IsDeleted { get; set; } = false;
    }
}
