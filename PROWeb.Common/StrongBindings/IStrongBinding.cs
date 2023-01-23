using PROWeb.Common.StrongBindings.Converters;
using PROWeb.Common.StrongBindings.Enums;

namespace PROWeb.Common.StrongBindings;

/// <summary>
/// Base binding interface.
/// </summary>
public interface IStrongBinding<in TSource, in TTarget>
{
    /// <summary>
    /// Create <see cref="StrongInstanceBinding{TSource,TTarget,TSourceProperty,TTargetProperty}" /> instance.
    /// </summary>
    /// <param name="source">Source instance.</param>
    /// <param name="target">Target instance.</param>
    /// <returns></returns>
    public IStrongInstanceBinding Bind(TSource source, TTarget target);
}

/// <summary>
/// Base <see cref="StrongBinding{TSource,TTarget,TSourceProperty,TTargetProperty}" /> interface.
/// </summary>
public interface IStrongBinding<TSource, TTarget, TSourceProperty, TTargetProperty> : IStrongBinding<TSource, TTarget>
{
    /// <summary>
    /// Binding mode to use.
    /// </summary>
    StrongBindingMode Mode { get; set; }

    /// <summary>
    /// Property converter.
    /// </summary>
    IStrongValueConverter<TSourceProperty, TTargetProperty>? Converter { get; set; }

    /// <summary>
    /// Source property path.
    /// </summary>
    StrongBindingPath<TSource, TSourceProperty> SourcePath { get; set; }

    /// <summary>
    /// Target property path.
    /// </summary>
    StrongBindingPath<TTarget, TTargetProperty> TargetPath { get; set; }
}

/// <summary>
/// Base <see cref="StrongBinding{TSource,TTarget,TProperty}" /> interface where <see cref="TSource" /> property type is same as
/// <see cref="TTarget" /> property type.
/// </summary>
public interface IStrongBinding<TSource, TTarget, TProperty> : IStrongBinding<TSource, TTarget>
{
    /// <summary>
    /// Binding mode to use.
    /// </summary>
    StrongBindingMode Mode { get; set; }

    /// <summary>
    /// Source property path.
    /// </summary>
    StrongBindingPath<TSource, TProperty> SourcePath { get; set; }

    /// <summary>
    /// Target property path.
    /// </summary>
    StrongBindingPath<TTarget, TProperty> TargetPath { get; set; }

    /// <summary>
    /// Property converter.
    /// </summary>
    IStrongValueConverter<TProperty, TProperty>? Converter { get; set; }
}
