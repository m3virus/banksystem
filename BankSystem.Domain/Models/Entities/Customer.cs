using BankSystem.Domain.Extensions;
using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;
using BankSystem.Domain.Statics;

namespace BankSystem.Domain.Models.Entities
{
    public class Customer : BaseEntity
    {
        #region Properties
        public string UserName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }

        public string PersianBirtdate
            => BirthDate.GeorgianToPersian(DateTimeFarmatStatics.Date);
        public UserTypeEnum UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        #endregion

        #region Relations

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        #endregion

    }
}
