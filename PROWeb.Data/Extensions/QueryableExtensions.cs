using System.Linq.Expressions;

namespace PROWeb.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WhereIfNotNull<TEntity, TValue>(this IQueryable<TEntity> querable, TValue? value, Expression<Func<TEntity, bool>> predicate)
            where TValue : struct
        {
            return querable = value.HasValue ? querable.Where(predicate) : querable;
        }

        public static IQueryable<TEntity> WhereIfNotNull<TEntity>(this IQueryable<TEntity> querable, string? value, Expression<Func<TEntity, bool>> predicate)
        {
            return querable = !string.IsNullOrWhiteSpace(value) ? querable.Where(predicate) : querable;
        }
    }
}
