using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Infrastructure.Extensions
{
    public static class TransactionNumberGeneratorExtensions
    {
        public static string GenerateTwentyDigitString(this string model)
        {
            var length = model.Length;

            var random = new Random().Next(100000,999999).ToString();

            var result = model + random;

            return result;
        }
    }
}
