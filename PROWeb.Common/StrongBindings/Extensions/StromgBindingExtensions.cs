using PROWeb.Common.StrongBindings.Converters;
using PROWeb.Common.StrongBindings.Enums;
using System.Linq.Expressions;

namespace PROWeb.Common.StrongBindings.Extensions
{
    public static class StrongBindingExtensions
    {
        public static IStrongInstanceBinding<TSource, TTarget, TSourceProperty, TTargetProperty>? Bind<TSource, TTarget, TSourceProperty, TTargetProperty>(this
            TSource source,
            TTarget target,
            Expression<Func<TSource, TSourceProperty>>? sourcePathExpression,
            Expression<Func<TTarget, TTargetProperty>>? targetPathExpression,
            StrongBindingMode mode = StrongBindingMode.OneWay,
            IStrongValueConverter<TSourceProperty, TTargetProperty>? converter = null)
        {
            if(sourcePathExpression is not { }  || targetPathExpression is not { })
            {
                return null;
            }

            StrongBindingPath<TSource, TSourceProperty> sourcePath = new StrongBindingPath<TSource, TSourceProperty>(sourcePathExpression);
            StrongBindingPath<TTarget, TTargetProperty> targetPath = new StrongBindingPath<TTarget, TTargetProperty>(targetPathExpression);

            if (mode == StrongBindingMode.OneWayToSource || mode == StrongBindingMode.TwoWay)
            {
                sourcePath.CachePropertyGetterAndSetter();
            }
            else
            {
                sourcePath.CachePropertyGetterAndSetter(true, false);
            }

            if (mode == StrongBindingMode.OneWayToSource)
            {
                targetPath.CachePropertyGetterAndSetter(true, false);
            }
            else
            {
                targetPath.CachePropertyGetterAndSetter();
            }

            return new StrongInstanceBinding<TSource, TTarget, TSourceProperty, TTargetProperty>(
                source,
                target,
                sourcePath,
                targetPath,
                mode,
                converter);
        }
    }
}
