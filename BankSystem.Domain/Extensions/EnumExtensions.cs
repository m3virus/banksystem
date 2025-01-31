using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string ToEnumTitle(this Enum? obj)
        {
            if (obj == null)
            {
                return "";
            }

            try
            {
                var enumType = obj.GetType();
                var fi = enumType.GetField(obj.ToString()!);
                if (fi == null)
                {
                    return "";
                }

                var attributeDescription = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                var attributeDisplay = fi.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();

                if (attributeDescription != null)
                {
                    return ((DescriptionAttribute)attributeDescription).Description;
                }

                if (attributeDisplay != null)
                {
                    return ((DisplayAttribute)attributeDisplay).Name!;
                }

                return fi.Name;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static TEnum ToEnum<TEnum>(this string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
