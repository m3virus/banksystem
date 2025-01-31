using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Statics
{
    public class RegexStatic
    {
        public const string Mobile = @"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$";
        public const string Email = @"[a-zA-Z0-9.-]+@[a-z-]+\.[a-z]{2,3}";

    }
}
