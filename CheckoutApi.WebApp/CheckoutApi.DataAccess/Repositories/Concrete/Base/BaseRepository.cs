using CheckoutApi.DataAccess.Contect.Interfaces;
using CheckoutApi.DataAccess.Helpers;
using CheckoutApi.DataAccess.Repositories.Interfaces.Base;
using CheckoutApi.DataModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CheckoutApi.DataAccess.Repositories.Concrete.BaseRepository
{
    public abstract partial class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ICheckoutDbContext CheckoutDbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected BaseRepository(ICheckoutDbContext checkoutDbContext)
        {
            CheckoutDbContext = checkoutDbContext;
            DbSet = checkoutDbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual async Task<TEntity> GetByAsync(IEnumerable<Expression<Func<TEntity, bool>>> whereFilters,
           Expression<Func<TEntity, object>>[] includes = null,
           bool asNoTracking = false)
        {
            return await DbSet
              .IncludeMultiple(includes)
              .ConfigureAsNoTracking(asNoTracking)
              .CombineWhereFilters(whereFilters)
              .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[] includes = null, bool asNoTracking = false)
        {
            return await DbSet
             .IncludeMultiple(includes)
             .ConfigureAsNoTracking(asNoTracking)
             .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByAsync(IEnumerable<Expression<Func<TEntity, bool>>> whereFilters,
            Expression<Func<TEntity, object>>[] includes = null,
            bool asNoTracking = false)
        {
            return await DbSet.IncludeMultiple(includes)
              .CombineWhereFilters(whereFilters)
              .ConfigureAsNoTracking(asNoTracking)
              .ToListAsync();
        }
    }
}
