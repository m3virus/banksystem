using BankSystem.Domain.Models.Base;

namespace BankSystem.Domain.Models.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
