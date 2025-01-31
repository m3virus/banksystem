using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Infrastructure.Options
{
    public class BankInfoOption
    {
        public string BankCode { get; set; }
        public string BankTax { get; set; }
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
    }
}
