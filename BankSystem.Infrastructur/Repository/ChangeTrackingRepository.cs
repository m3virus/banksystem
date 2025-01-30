using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;

namespace BankSystem.Infrastructure.Repository
{
    public class ChangeTrackingRepository : BaseRepository<ChangeTracking>, IChangeTrackingRepository
    {
        public ChangeTrackingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
