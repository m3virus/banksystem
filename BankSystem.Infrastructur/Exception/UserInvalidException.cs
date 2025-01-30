
namespace BankSystem.Infrastructure.Exception
{
    public class UserInvalidException:System.Exception
    {
        public UserInvalidException() { }

        public UserInvalidException(string message)
            : base(message)
        {
        }

        public UserInvalidException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
