using BankSystem.Domain.Models.Base;

namespace BankSystem.Domain.Models.Entities
{
    public class User : BaseEntity
    {
        #region Properties

        public string UserName { get; set; }
        public string Password { get; set; }

        #endregion

        #region Relations

        public virtual ICollection<ChangeTracking> ChangeTrackings { get; set; }

        #endregion

    }
}
