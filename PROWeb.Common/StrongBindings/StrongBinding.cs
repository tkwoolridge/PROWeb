using System.Linq.Expressions;
using PROWeb.Common.StrongBindings.Converters;
using PROWeb.Common.StrongBindings.Enums;

namespace PROWeb.Common.StrongBindings;

/// <summary>
/// Main binding implementation.
/// </summary>
public class StrongBinding<TSource, TTarget, TSourceProperty, TTargetProperty> : IStrongBinding<TSource, TTarget, TSourceProperty,
    TTargetProperty>
{
    public StrongBinding(
        Expression<Func<TSource, TSourceProperty>> sourcePath,
        Expression<Func<TTarget, TTargetProperty>> targetPath,
        StrongBindingMode mode = StrongBindingMode.OneWay,
        IStrongValueConverter<TSourceProperty, TTargetProperty>? converter = null)
    {
        Mode = mode;
        Converter = converter;

        SourcePath = new StrongBindingPath<TSource, TSourceProperty>(sourcePath);
        TargetPath = new StrongBindingPath<TTarget, TTargetProperty>(targetPath);

        if (Mode == StrongBindingMode.OneWayToSource || Mode == StrongBindingMode.TwoWay)
        {
            SourcePath.CachePropertyGetterAndSetter();
        }
        else
        {
            SourcePath.CachePropertyGetterAndSetter(true, false);
        }

        if (Mode == StrongBindingMode.OneWayToSource)
        {
            TargetPath.CachePropertyGetterAndSetter(true, false);
        }
        else
        {
            TargetPath.CachePropertyGetterAndSetter();
        }
    }

    public StrongBindingMode Mode { get; set; }

    public IStrongValueConverter<TSourceProperty, TTargetProperty>? Converter { get; set; }

    public StrongBindingPath<TSource, TSourceProperty> SourcePath { get; set; }

    public StrongBindingPath<TTarget, TTargetProperty> TargetPath { get; set; }

    public virtual IStrongInstanceBinding Bind(TSource source, TTarget target)
    {
        return new StrongInstanceBinding<TSource, TTarget, TSourceProperty, TTargetProperty>(
            source,
            target,
            SourcePath,
            TargetPath,
            Mode,
            Converter);
    }
}

/// <summary>
/// Binding implementation where <see cref="TSource" /> property type is same as <see cref="TTarget" /> property type.
/// </summary>
public class StrongBinding<TSource, TTarget, TProperty> : StrongBinding<TSource, TTarget, TProperty, TProperty>,
    IStrongBinding<TSource, TTarget, TProperty>
{
    public StrongBinding(
        Expression<Func<TSource, TProperty>> sourcePath,
        Expression<Func<TTarget, TProperty>> targetPath,
        StrongBindingMode mode = StrongBindingMode.OneWay
    ) : base(sourcePath, targetPath, mode)
    {
    }
}
