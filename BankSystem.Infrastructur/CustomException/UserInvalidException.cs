
namespace BankSystem.Infrastructure.CustomException
{
    public class UserInvalidException:Exception
    {
        public UserInvalidException() { }

        public UserInvalidException(string message)
            : base(message)
        {
        }

        public UserInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
