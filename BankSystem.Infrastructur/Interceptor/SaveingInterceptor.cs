using BankSystem.Domain.Models.Base;
using BankSystem.Domain.Models.Entities;
using BankSystem.Infrastructure.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BankSystem.Infrastructure.Interceptor
{
    public class SaveingInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;
        public SaveingInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            SetDetails(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new())
        {
            SetDetails(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void SetDetails(DbContext? dbContext)
        {
            if (dbContext == null) return;

            foreach (var entry in dbContext.ChangeTracker.Entries<BaseEntity>())
            {
                var type = entry.Entity.GetType().Name;

                if (type == nameof(ChangeTracking))
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }
        }
    }
}
