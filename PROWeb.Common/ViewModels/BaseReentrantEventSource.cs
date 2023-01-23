using System.Diagnostics.CodeAnalysis;

namespace PROWeb.Common.ViewModels
{
    /// <summary>
    /// Deals with subscribing/unsubscribing of event handlers and makes sure reentrant
    /// calls do not interfere when an event is being dispatched.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="BaseDoubleBufferedEventSource{TDelegate}"/>, this event source uses
    /// a basic list, counting reentering fires, ignoring entries marked as deleted,
    /// and pruning when leaving the outermost loop.
    /// </remarks>
    /// <typeparam name="TDelegate">The type of the event listening delegates.</typeparam>
    /// <typeparam name="TArgs">A type containing all arguments to be passed to the subscribers. This can be a tuple.</typeparam>
    public abstract class BaseReentrantEventSource<TDelegate, TArgs> where TDelegate : Delegate where TArgs : struct
    {
        private int _alive;
        private int _depth;
        private bool _needsPrune;
        private List<Subscription>? _subscriptions;

        /// <summary>
        /// Whether this event source has any subscriptions or not.
        /// </summary>
        [MemberNotNullWhen(true, nameof(_subscriptions))]
        public bool HasSubscriptions => _alive > 0;

        /// <summary>
        /// Adds a new subscription.
        /// </summary>
        /// <param name="delegate">The callback to add.</param>
        public void Subscribe(TDelegate @delegate)
        {
            _alive++;
            _subscriptions ??= new List<Subscription>();
            _subscriptions.Add(new Subscription(@delegate));
        }

        /// <summary>
        /// Removes an existing subscription, or marks it for deletion when all fire loops are finished.
        /// </summary>
        /// <param name="delegate">The callback to remove.</param>
        public void Unsubscribe(TDelegate @delegate)
        {
            if (_subscriptions is null or { Count: 0 })
            {
                return;
            }

            for (int i = _subscriptions.Count - 1; i >= 0; i--)
            {
                Subscription subscription = _subscriptions[i];

                if (subscription.Delegate != @delegate || subscription.IsMarkedForRemoval)
                {
                    continue;
                }

                _alive--;

                if (_depth > 0)
                {
                    _needsPrune = true;
                    _subscriptions[i] = subscription with
                    {
                        DiedAt = _depth
                    };
                }
                else
                {
                    _subscriptions.RemoveAt(i);
                }

                return;
            }
        }

        /// <summary>
        /// The method to call from your <c>Fire</c> method.
        /// </summary>
        /// <param name="args">The arguments to pass to each active subscriber.</param>
        protected void FireCore(in TArgs args)
        {
            if (!HasSubscriptions)
            {
                return;
            }

            int count = _subscriptions.Count;
            int depth = ++_depth;

            try
            {
                for (int i = 0; i < count; i++)
                {
                    Subscription subscription = _subscriptions[i];

                    if (subscription.IsAlive(depth))
                    {
                        Invoke(subscription.Delegate, args);
                    }
                }
            }
            finally
            {
                if (--_depth <= 0 && _subscriptions is not null && _needsPrune)
                {
                    _needsPrune = false;

                    for (int i = _subscriptions.Count - 1; i >= 0; i--)
                    {
                        if (_subscriptions[i].IsMarkedForRemoval)
                        {
                            _subscriptions.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The method called by <see cref="FireCore"/> which invokes the given subscriber.
        /// </summary>
        /// <param name="delegate">The subscriber to invoke.</param>
        /// <param name="args">The arguments to invoke it with.</param>
        protected abstract void Invoke(TDelegate @delegate, in TArgs args);

        /// <summary>
        /// An entry in <see cref="BaseReentrantEventSource{TDelegate,TArgs}._subscriptions"/>
        /// </summary>
        /// <param name="Delegate">The delegate to invoke if this entry is still alive.</param>
        /// <param name="DiedAt">The depth at which this was unsubscribed, or <see cref="int.MaxValue"/> if still alive.</param>
        private readonly record struct Subscription(TDelegate Delegate, int DiedAt = int.MaxValue)
        {
            /// <summary>
            /// Whether or not the subscription has been marked for removal during any fire loop.
            /// </summary>
            public bool IsMarkedForRemoval => DiedAt != int.MaxValue;

            /// <summary>
            /// Whether this subscription is still alive or not.
            /// </summary>
            /// <param name="depth">The depth of the current loop.</param>
            /// <returns>True if this subscription's delegate should be invoked, false otherwise.</returns>
            public bool IsAlive(int depth)
            {
                return DiedAt >= depth;
            }
        }
    }
}