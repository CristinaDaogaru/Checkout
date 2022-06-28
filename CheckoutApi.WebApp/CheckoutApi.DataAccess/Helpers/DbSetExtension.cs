using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CheckoutApi.DataAccess.Helpers
{
    public static class DbSetExtension
    {
        public static IQueryable<T> IncludeMultiple<T>(this DbSet<T> dbSet,
          params Expression<Func<T, object>>[] includes)
          where T : class
        {
            IQueryable<T> query = dbSet;

            if (includes is null)
            {
                return query;
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public static IQueryable<T> CombineWhereFilters<T>(this IQueryable<T> dbSet, IEnumerable<Expression<Func<T, bool>>> whereFilters)
          where T : class
        {
            var query = dbSet;

            foreach (var whereFilter in whereFilters)
            {
                query = query.Where(whereFilter).AsQueryable();
            }

            return query;
        }

        public static IQueryable<T> ConfigureAsNoTracking<T>(this IQueryable<T> dbSet, bool asNoTracking)
          where T : class
        {
            return asNoTracking
              ? dbSet.AsNoTracking()
              : dbSet;
        }
    }
}
