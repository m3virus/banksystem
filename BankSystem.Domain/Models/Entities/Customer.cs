using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Enums;

namespace BankSystem.Domain.Models.Entities
{
    public class Customer:BaseEntity
    {
        public string UserName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
