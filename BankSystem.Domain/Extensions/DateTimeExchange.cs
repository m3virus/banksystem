using System.Globalization;
using BankSystem.Domain.Statics;

namespace BankSystem.Domain.Extensions
{
    public static class DateTimeExchange
    {
        
        public static string GeorgianToPersian(this DateTime dateTime, string format)
        {
            //Create Culture Info Instance For Persian
            var faCultureInfo = new CultureInfo("fa-IR");

            // Create a PersianCalendar Instance
            faCultureInfo.DateTimeFormat.Calendar = new PersianCalendar();

            return dateTime.ToString(format, faCultureInfo);
        }

        public static DateTime ConvertPersianToGregorian(this string persianDate, string format)
        {
            DateTime gregorianDate = new DateTime();
            if (format is DateTimeFarmatStatics.Date)
            {
                // Parse The Persian Date String
                string[] dateParts = persianDate.Split('/');
                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);

                // Create a PersianCalendar Instance
                PersianCalendar persianCalendar = new PersianCalendar();

                // Convert To DateTime
                gregorianDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            else if (format is DateTimeFarmatStatics.DateAndHour)
            {
                //Separate Time And Date
                string date = persianDate.Split(" ")[0];
                string time = persianDate.Split(" ")[1];
                // Parse the Persian Date String
                string[] dateParts = date.Split('/');
                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);

                string[] hourParts = time.Split(":");
                int hour = int.Parse(hourParts[0]);
                int minute = int.Parse(hourParts[1]);
                int second = int.Parse(hourParts[2]);
                // Create a PersianCalendar Instance
                PersianCalendar persianCalendar = new PersianCalendar();

                // Convert To DateTime
                gregorianDate = persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
            }
            else
            {
                return DateTime.MinValue;
            }
            
            

            return gregorianDate;
        }
    }
}
