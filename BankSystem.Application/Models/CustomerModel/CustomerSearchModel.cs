using BankSystem.Domain.Models.Enums;
using BankSystem.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Models.CustomerModel
{
    public class CustomerSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersianBirtdate { get; set; }
        public string UserTypeTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public string AccountStatusTitle { get; set; }
        public double? TransactionValue { get; set; } = null;
        public string? TransactionType { get; set; } = null;
        public string? TransactionNumber { get; set; } = null;
    }
}
