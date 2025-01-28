using BankSystem.Domain.Models.Base;

namespace BankSystem.Domain.Models.Entities
{
    public class ChangeTracking : BaseEntity
    {
        #region Properties

        public string Entity { get; set; }
        public string Status { get; set; }

        #endregion

        #region Relation

        public Guid UserId { get; set; }
        public User User { get; set; }

        #endregion

    }
}
