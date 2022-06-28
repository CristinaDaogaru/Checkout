using CheckoutApi.DataModels.Base;
using System.Linq.Expressions;

namespace CheckoutApi.DataAccess.Repositories.Interfaces.Base
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);

        Task<TEntity> GetByAsync(IEnumerable<Expression<Func<TEntity, bool>>> whereFilters,
         Expression<Func<TEntity, object>>[] includes = null,
         bool asNoTracking = false);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[] includes = null,
             bool asNoTracking = false);

        Task<IEnumerable<TEntity>> GetAllByAsync(IEnumerable<Expression<Func<TEntity, bool>>> whereFilters,
            Expression<Func<TEntity, object>>[] includes = null,
            bool asNoTracking = false);

    }
}
