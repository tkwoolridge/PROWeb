using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PROWeb.Common.Extensions;

public static class LambdaExtensions
{
    public static string GetPropertyName<TOwner, TValue>(this Expression<Func<TOwner, TValue>> property)
    {
        return property.GetPropertyInfo().Name;
    }

    public static string GetPropertyPath<TOwner, TValue>(this Expression<Func<TOwner, TValue>> property)
    {
        var memExpr = (MemberExpression)property.Body;

        var parts = new List<string>();

        while (memExpr != null)
        {
            parts.Add(memExpr.Member.Name);
            memExpr = memExpr.Expression as MemberExpression;
        }

        parts.Reverse();

        return string.Join(".", parts);
    }

    public static PropertyInfo GetPropertyInfo<TOwner, TValue>(this Expression<Func<TOwner, TValue>> property)
    {
        if (property.Body is UnaryExpression unaryExpression)
        {
            return (PropertyInfo)((MemberExpression)unaryExpression.Operand).Member;
        }

        var memExpr = (MemberExpression)property.Body;

        return (PropertyInfo)memExpr.Member;
    }

    public static (object?, PropertyInfo) GetPropertyInfo<TProvider, TValue>(
        this TProvider provider,
        Expression<Func<TProvider, TValue>> property)
    {
        MemberExpression memExpr = property.Body is UnaryExpression unaryExpression
            ? (MemberExpression)unaryExpression.Operand
            : (MemberExpression)property.Body;
        var pInfo = (PropertyInfo)memExpr.Member;

        if (memExpr.Expression is MemberExpression ownerExpr)
        {
            //For nested expressions we also need to retrieve the direct parent of
            //the property. This is required to invoke the setter later on.
            return (Expression
                .Lambda(ownerExpr, property.Parameters)
                .Compile()
                .DynamicInvoke(provider), pInfo);
        }

        return (provider, pInfo);
    }

    /// <summary>
    /// Writes to a property on the given object.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="owner">The object to write the property on</param>
    /// <param name="propertyName">The property name to write to</param>
    /// <param name="value">The value to write</param>
    public static void WriteProperty<TOwner, TValue>(
        this TOwner owner,
        string propertyName,
        TValue? value)
    {
        PropertyInfo? propertyInfo = owner?.GetType().GetProperty(propertyName);

        Debug.Assert(propertyInfo != null);

        owner.WriteProperty(propertyInfo, value);
    }

    /// <summary>
    /// Writes to a property on the given object.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="owner">The object to write the property on</param>
    /// <param name="property">The property expression to write to</param>
    /// <param name="value">The value to write</param>
    public static void WriteProperty<TOwner, TValue>(
        this TOwner owner,
        Expression<Func<TOwner, TValue?>> property,
        TValue? value)
    {
        PropertyInfo propertyInfo = property.GetPropertyInfo();

        owner.WriteProperty(propertyInfo, value);
    }

    /// <summary>
    /// Writes to a property on the given object.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="owner">The object to write the property on</param>
    /// <param name="propertyInfo">The property info to write to</param>
    /// <param name="value">The value to write</param>
    public static void WriteProperty<TOwner, TValue>(
        this TOwner owner,
        PropertyInfo propertyInfo,
        TValue? value)
    {
        ref DelegateCache<TOwner>.Delegates delegates
            = ref DelegateCache<TOwner>.Lookup(propertyInfo.Name);

        if (delegates.Setter == null && propertyInfo.CanWrite)
        {
            ParameterExpression arg = Expression.Parameter(typeof(TOwner));
            ParameterExpression arg2 = Expression.Parameter(typeof(TValue));
            delegates.Setter = Expression.Lambda<Action<TOwner, TValue?>>(
                Expression.Assign(
                    Expression.MakeMemberAccess(arg, propertyInfo),
                    Expression.Convert(arg2, propertyInfo.PropertyType)),
                arg,
                arg2).Compile();
        }

        (delegates.Setter as Action<TOwner, TValue?>)?.Invoke(owner, value);
    }

    /// <summary>
    /// Reads a property from a given object.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="owner">The object to read the property from</param>
    /// <param name="propertyName">The property name</param>
    public static TValue? ReadProperty<TOwner, TValue>(
        this TOwner owner,
        string propertyName)
    {
        PropertyInfo? propertyInfo = owner?.GetType().GetProperty(propertyName);

        Debug.Assert(propertyInfo != null);

        return owner.ReadProperty<TOwner, TValue>(propertyInfo);
    }

    /// <summary>
    /// Reads a property from a given object.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="owner">The object to read the property from</param>
    /// <param name="property">The property expression to read</param>
    public static TValue? ReadProperty<TOwner, TValue>(
        this TOwner owner,
        Expression<Func<TOwner, TValue>> property)
    {
        PropertyInfo propertyInfo = property.GetPropertyInfo();

        return owner.ReadProperty<TOwner, TValue>(propertyInfo);
    }

    /// <summary>
    /// Reads a property from a given object.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="owner">The object to read the property from</param>
    /// <param name="propertyInfo">The property info to read</param>
    public static TValue? ReadProperty<TOwner, TValue>(
        this TOwner owner,
        PropertyInfo propertyInfo)
    {
        ref DelegateCache<TOwner>.Delegates delegates
            = ref DelegateCache<TOwner>.Lookup(propertyInfo.Name);

        if (delegates.Getter == null)
        {
            ParameterExpression arg = Expression.Parameter(typeof(TOwner));
            delegates.Getter = Expression.Lambda<Func<TOwner, TValue>>(
                Expression.Convert(Expression.Property(arg, propertyInfo), typeof(TValue)),
                arg).Compile();
        }

        return ((Func<TOwner, TValue?>)delegates.Getter)(owner);
    }

    private static class DelegateCache<TOwner>
    {
        public struct Delegates
        {
            public Delegates(Delegate? getter, Delegate? setter)
            {
                Getter = getter;
                Setter = setter;
            }

            public Delegate? Getter { get; set; }

            public Delegate? Setter { get; set; }
        }

        private static readonly Dictionary<string, Delegates> _cache = new();

        public static ref Delegates Lookup(string propertyName)
        {
            return ref CollectionsMarshal.GetValueRefOrAddDefault(
                _cache,
                propertyName,
                out _);
        }
    }
}
