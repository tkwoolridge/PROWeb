using ReactiveUI;
using System.ComponentModel;

namespace PROWeb.Common.ViewModels
{
    /// <summary>
    /// A reactive object like <see cref="ReactiveObject" />, but with a smaller footprint.
    /// </summary>
    public interface ISlimReactiveObject : IReactiveObject
    {
        /// <summary>
        /// Subscribes an action to a property, invoking it when the property changes.
        /// </summary>
        /// <param name="p1">The name of the property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>A <see cref="Subscription" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Subscribes an action to multiple properties, invoking it when any of the properties change.
        /// </summary>
        /// <param name="p1">The name of the first property.</param>
        /// <param name="p2">The name of the second property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            string p2,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Subscribes an action to multiple properties, invoking it when any of the properties change.
        /// </summary>
        /// <param name="p1">The name of the first property.</param>
        /// <param name="p2">The name of the second property.</param>
        /// <param name="p3">The name of the third property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Subscribes an action to multiple properties, invoking it when any of the properties change.
        /// </summary>
        /// <param name="p1">The name of the first property.</param>
        /// <param name="p2">The name of the second property.</param>
        /// <param name="p3">The name of the third property.</param>
        /// <param name="p4">The name of the fourth property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Subscribes an action to multiple properties, invoking it when any of the properties change.
        /// </summary>
        /// <param name="p1">The name of the first property.</param>
        /// <param name="p2">The name of the second property.</param>
        /// <param name="p3">The name of the third property.</param>
        /// <param name="p4">The name of the fourth property.</param>
        /// <param name="p5">The name of the fifth property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Subscribes an action to multiple properties, invoking it when any of the properties change.
        /// </summary>
        /// <param name="p1">The name of the first property.</param>
        /// <param name="p2">The name of the second property.</param>
        /// <param name="p3">The name of the third property.</param>
        /// <param name="p4">The name of the fourth property.</param>
        /// <param name="p5">The name of the fifth property.</param>
        /// <param name="p6">The name of the sixth property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            string p6,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Subscribes an action to multiple properties, invoking it when any of the properties change.
        /// </summary>
        /// <param name="p1">The name of the first property.</param>
        /// <param name="p2">The name of the second property.</param>
        /// <param name="p3">The name of the third property.</param>
        /// <param name="p4">The name of the fourth property.</param>
        /// <param name="p5">The name of the fifth property.</param>
        /// <param name="p6">The name of the sixth property.</param>
        /// <param name="p7">The name of the seventh property.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="initial">Whether the action should be invoked immediately after subscribing.</param>
        /// <param name="beforeChange">Whether the action should be invoked prior to changing the value of the property.</param>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the subscription.</returns>
        Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            string p6,
            string p7,
            Action action,
            bool initial = true,
            bool beforeChange = false);

        /// <summary>
        /// Temporarily suspends the invoking of subscribed actions for this instance.
        /// </summary>
        /// <returns>An <see cref="IDisposable" /> instance, indicating the lifetime of the suspension.</returns>
        IDisposable SuspendSubscriptions();

        public readonly struct Subscription : IDisposable
        {
            private readonly ISlimReactiveObject _target;
            private readonly object _handler;

            internal Subscription(ISlimReactiveObject target, PropertyChangedEventHandler handler)
            {
                _target = target;
                _handler = handler;
            }

            internal Subscription(ISlimReactiveObject target, PropertyChangingEventHandler handler)
            {
                _target = target;
                _handler = handler;
            }

            public void Dispose()
            {
                switch (_handler)
                {
                    case PropertyChangedEventHandler h:
                        _target.PropertyChanged -= h;

                        break;

                    case PropertyChangingEventHandler h:
                        _target.PropertyChanging -= h;

                        break;
                }
            }
        }
    }
}