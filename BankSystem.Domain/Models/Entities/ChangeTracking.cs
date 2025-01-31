using BankSystem.Domain.Models.Base;

namespace BankSystem.Domain.Models.Entities
{
    public class ChangeTracking : BaseEntity
    {
        #region Properties

        public string Entity { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        #endregion


    }
}
