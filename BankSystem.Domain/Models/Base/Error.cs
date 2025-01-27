using System.Xml;

namespace BankSystem.Domain.Models.Base
{
    public record Error(string Name,  string Message)
    {
        public static Error NullVallue = new("Error.NullValue", "Null Value Provider");
        public static Error None = new(string.Empty, string.Empty);
    }
}
