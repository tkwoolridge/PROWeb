using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using FastExpressionCompiler;
using PROWeb.Common.Extensions;

namespace PROWeb.Common.StrongBindings;

/// <summary>
/// Binding path implementation.
/// </summary>
public class StrongBindingPath<TSource, TProperty> : IStrongBindingPath
{
    private readonly PropertyInfo _property;

    private Func<TSource, TProperty?>? _getter;

    private Action<TSource, TProperty?>? _setter;

    public StrongBindingPath(Expression<Func<TSource, TProperty>> path)
    {
        Path = path;
        _property = path.GetPropertyInfo();
    }

    /// <summary>
    /// Path property name.
    /// </summary>
    public string PropertyName => _property.Name;

    /// <summary>
    /// Path expression.
    /// </summary>
    public Expression<Func<TSource, TProperty>> Path { get; }

    /// <summary>
    /// Get/read property value.
    /// </summary>
    /// <param name="source">Instance of <see cref="TSource" />.</param>
    public TProperty? ReadProperty(TSource source)
    {
        Debug.Assert(_getter != null);

        return _getter.Invoke(source);
    }

    /// <summary>
    /// Set/write property value.
    /// </summary>
    /// <param name="source">Instance of <see cref="TSource" />.</param>
    /// <param name="value"><see cref="TProperty" /> value to write.</param>
    public void WriteProperty(TSource source, TProperty? value)
    {
        _setter?.Invoke(source, value);
    }

    internal void CachePropertyGetterAndSetter(bool canRead = true, bool canWrite = true)
    {
        if (canRead)
        {
            _getter = GetGetter(_property);
        }

        if (canWrite && _property.CanWrite)
        {
            _setter = GetSetter(_property);
        }
    }

    private static Func<TSource, TProperty?> GetGetter(PropertyInfo property)
    {
        ParameterExpression arg = Expression.Parameter(typeof(TSource));
        Func<TSource, TProperty> result = Expression.Lambda<Func<TSource, TProperty>>(
            Expression.Convert(Expression.Property(arg, property), typeof(TProperty)),
            arg).CompileFast();

        return result;
    }

    private static Action<TSource, TProperty?> GetSetter(PropertyInfo property)
    {
        ParameterExpression arg = Expression.Parameter(typeof(TSource));
        ParameterExpression arg2 = Expression.Parameter(typeof(TProperty));
        Action<TSource, TProperty?> result = Expression.Lambda<Action<TSource, TProperty?>>(
            Expression.Assign(
                Expression.MakeMemberAccess(arg, property),
                Expression.Convert(arg2, property.PropertyType)),
            arg,
            arg2).CompileFast();

        return result;
    }

    object? IStrongBindingPath.ReadProperty(object source)
    {
        if (source is not TSource tSource)
        {
            throw new ArgumentException($"Property source must be of type {typeof(TSource)}");
        }

        return ReadProperty(tSource);
    }

    void IStrongBindingPath.WriteProperty(object source, object? value)
    {
        if(source is not TSource tSource)
        {
            throw new ArgumentException($"Property source must be of type {typeof(TSource)}");
        }

        if (value is not TProperty tValue)
        {
            throw new ArgumentException($"Property source must be of type {typeof(TSource)}");
        }

        WriteProperty(tSource, tValue);
    }
}
