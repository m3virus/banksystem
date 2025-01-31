using BankSystem.Domain.Extensions;
using BankSystem.Domain.Statics;

namespace BankSystem.Infrastructure.Extensions
{
    public class BankAccountNumberGenerator
    {
        public static string Generate()
        {
            var date = DateTime.Now.GeorgianToPersian(DateTimeFormatStatics.SpecifiedForGeneration);
            return date;
        }
    }
}
