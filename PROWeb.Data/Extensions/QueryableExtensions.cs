using Humanizer;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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

        public static SqlParameter ToSqlParameter<TValue>(this TValue? value, [CallerArgumentExpression("value")] string name = "")
        {
            object pValue = value switch
            {
                null => DBNull.Value,
                _ => value
            };

            return new SqlParameter($"@{name.Humanize(LetterCasing.Title).Replace(" ","")}", pValue);
        }
    }
}
