using PROWeb.Common.StrongBindings;

namespace PROWeb.Common.StrongBindings.Converters
{
    /// <summary>
    /// Value Converter for <see cref="IStrongBinding{TSource,TTarget}"/>.
    /// </summary>
    public interface IStrongValueConverter<TSourceProperty, TTargetProperty>
    {
        /// <summary>
        /// Convert to source value.
        /// </summary>
        /// <param name="targetValue">Target value.</param>
        /// <returns></returns>
        TSourceProperty ToSourceProperty(TTargetProperty? targetValue);

        /// <summary>
        /// Convert to targetValue value.
        /// </summary>
        /// <param name="sourceValue">Source value.</param>
        /// <returns></returns>
        TTargetProperty ToTargetProperty(TSourceProperty? sourceValue);
    }
}