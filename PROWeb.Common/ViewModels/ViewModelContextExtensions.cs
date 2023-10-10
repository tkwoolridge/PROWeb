using FastExpressionCompiler;
using System.Linq.Expressions;
using System.Reflection;

namespace PROWeb.Common.ViewModels
{
    public static class ViewModelContextExtensions
    {
        public static Func<TContext, object?> GetGetter<TContext>(this TContext context, PropertyInfo property)
        {
            ParameterExpression arg = Expression.Parameter(typeof(TContext));
            Func<TContext, object> result = Expression.Lambda<Func<TContext, object>>(
                Expression.Convert(Expression.Property(arg, property), typeof(object)),
                arg).CompileFast();

            return result;
        }

        public static Action<TContext, object?> GetSetter<TContext>(this TContext _, PropertyInfo property)
        {
            ParameterExpression arg = Expression.Parameter(typeof(TContext));
            ParameterExpression arg2 = Expression.Parameter(typeof(object));
            Action<TContext, object?> result = Expression.Lambda<Action<TContext, object?>>(
                Expression.Assign(
                    Expression.MakeMemberAccess(arg, property),
                    Expression.Convert(arg2, property.PropertyType)),
                arg,
                arg2).CompileFast();

            return result;
        }
    }
}
