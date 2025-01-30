using System.Linq.Expressions;
using BankSystem.Domain.Models.Base;
using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repository
{
    public class BaseRepository<TEntity>:IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext DbContext;
        protected readonly DbSet<TEntity> Entities;

        public BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Entities.AsNoTracking().ToListAsync();
        }

        public ValueTask<TEntity?> GetByIdAsync(Guid id)
        {
            return Entities.FindAsync(id);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return (predicate != null
                ? Entities.AnyAsync(predicate)
                : Entities.AnyAsync());
        }

        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = Entities.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include)
                        => current.Include(include));
            }

            return predicate != null
                ? query.FirstOrDefaultAsync(predicate)
                : query.FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? predicate = null, bool disableTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                var query = Entities.AsQueryable().Where(x => !x.IsDeleted);

                if (disableTracking) query = query.AsNoTracking();

                if (includes is not null && includes.Any())
                    query = includes.Aggregate(query,
                        (current, include) => current.Include(include));

                if (predicate != null) query = query.Where(predicate);

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(TEntity).Name} ,  {ex}");
            }
        }

        public void Add(TEntity entity)
        {
            try
            {
                Entities.Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception(typeof(TEntity).Name, e);
            }
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            try
            {
                Entities.AddRange(entities);
            }
            catch (Exception e)
            {
                throw new Exception(typeof(TEntity).Name, e);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                Entities.Update(entity);
            }
            catch (Exception e)
            {
                throw new Exception(typeof(TEntity).Name, e);
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                Entities.Remove(entity);
            }
            catch (Exception e)
            {
                throw new Exception(typeof(TEntity).Name, e);
            }
        }
    }
}
