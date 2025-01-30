using System.Linq.Expressions;
using BankSystem.Domain.Models.Base;

namespace BankSystem.Infrastructure.IRepository
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        public IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includes);
        public void Add(TEntity entity);

        public void Add(IEnumerable<TEntity> entities);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);
    }
}
