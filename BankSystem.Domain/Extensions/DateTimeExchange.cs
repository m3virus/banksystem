using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Extensions
{
    public static class DateTimeExchange
    {
        
        public static string GeorgianToPersian(this DateTime dateTime, string format)
        {
            var faCultureInfo = new CultureInfo("fa-IR");
            faCultureInfo.DateTimeFormat.Calendar = new PersianCalendar();

            return dateTime.ToString("yyyy-MM-dd", faCultureInfo);
        }
    }
}
