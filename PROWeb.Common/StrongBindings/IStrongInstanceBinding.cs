using System;
using PROWeb.Common.StrongBindings.Converters;
using PROWeb.Common.StrongBindings.Enums;

namespace PROWeb.Common.StrongBindings;

/// <summary>
/// Binding instance interface.
/// </summary>
public interface IStrongInstanceBinding : IDisposable
{
    /// <summary>
    /// Suspend binding.
    /// </summary>
    void Suspend();

    /// <summary>
    /// Resume binding.
    /// </summary>
    void Resume();

    /// <summary>
    /// Sync source and target values.
    /// </summary>
    void Sync();
}

/// <summary>
/// Binding instance interface.
/// </summary>
public interface IStrongInstanceBinding<TSource, TTarget, TSourceProperty, TTargetProperty> : IStrongInstanceBinding
{
    /// <summary>
    /// Source instance.
    /// </summary>
    TSource Source { get; }

    /// <summary>
    /// Target instance.
    /// </summary>
    TTarget Target { get; }

    /// <summary>
    /// Binding mode to use.
    /// </summary>
    StrongBindingMode Mode { get; }

    /// <summary>
    /// Property converter.
    /// </summary>
    IStrongValueConverter<TSourceProperty, TTargetProperty>? Converter { get; }

    /// <summary>
    /// Source property path.
    /// </summary>
    StrongBindingPath<TSource, TSourceProperty> SourcePath { get; }

    /// <summary>
    /// Target property path.
    /// </summary>
    StrongBindingPath<TTarget, TTargetProperty> TargetPath { get; }
}
