using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Components;
using static PROWeb.Common.ViewModels.ISlimReactiveObject;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using PROWeb.Common.ViewModels;

namespace PROWeb.Common.Components
{
    public abstract class PROComponent : PROComponentBase, ISlimReactiveObject
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        [Inject]
        protected IMapper Mapper { get; set; } = null!;

        [CascadingParameter]
        public PROContentLayout? Layout { get; set; }

        [CascadingParameter]
        public PROLayout? MainLayout { get; set; }

        protected virtual string? PageTitle { get; private set; }

        protected override void OnInitialized()
        {
            if (IsRouted() &&
                MainLayout is { } layout)
            {
                PageTitle ??= _navigationManager.Uri.Split('/').Last().Humanize(LetterCasing.Title);

                MainLayout.SetPageTitle(PageTitle);
            }
        }

        private bool IsRouted()
        {
            var attributes = GetType().GetCustomAttributes(inherit: true);

            return attributes.OfType<RouteAttribute>().Any();
        }

        #region ISlimReactiveObject

        private readonly PropertyChangedReentrantEventSource _propertyChangedEventSource = new();
        private PropertyChangingReentrantEventSource? _propertyChangingEventSource;
        private bool _suspendSubscriptions;

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => _propertyChangedEventSource.Subscribe(value!);
            remove => _propertyChangedEventSource.Unsubscribe(value!);
        }

        public event PropertyChangingEventHandler? PropertyChanging
        {
            add
            {
                _propertyChangingEventSource ??= new PropertyChangingReentrantEventSource();
                _propertyChangingEventSource.Subscribe(value!);
            }
            remove => _propertyChangingEventSource?.Unsubscribe(value!);
        }

        public bool HasSubscriptions =>
            _propertyChangedEventSource.HasSubscriptions ||
            _propertyChangingEventSource is { HasSubscriptions: true };

        public Subscription SubscribeFast(
            string p1,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 || p == s.p2)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 ||
                        p == s.p2 ||
                        p == s.p3)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, p3, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 ||
                        p == s.p2 ||
                        p == s.p3 ||
                        p == s.p4)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, p3, p4, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 ||
                        p == s.p2 ||
                        p == s.p3 ||
                        p == s.p4 ||
                        p == s.p5)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, p3, p4, p5, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            string p6,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 ||
                        p == s.p2 ||
                        p == s.p3 ||
                        p == s.p4 ||
                        p == s.p5 ||
                        p == s.p6)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, p3, p4, p5, p6, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            string p6,
            string p7,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 ||
                        p == s.p2 ||
                        p == s.p3 ||
                        p == s.p4 ||
                        p == s.p5 ||
                        p == s.p6 ||
                        p == s.p7)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, p3, p4, p5, p6, p7, action));
        }

        public Subscription SubscribeFast(
            string p1,
            string p2,
            string p3,
            string p4,
            string p5,
            string p6,
            string p7,
            string p8,
            Action action,
            bool initial = true,
            bool beforeChange = false)
        {
            if (initial)
            {
                action();
            }

            return SubscribeCore(
                static (p, s) =>
                {
                    if (p == s.p1 ||
                        p == s.p2 ||
                        p == s.p3 ||
                        p == s.p4 ||
                        p == s.p5 ||
                        p == s.p6 ||
                        p == s.p7 ||
                        p == s.p8)
                    {
                        s.action();
                    }
                },
                beforeChange,
                (p1, p2, p3, p4, p5, p6, p7, p8, action));
        }

        public void RaisePropertyChanging(string propertyName)
        {
            RaisePropertyChanging(new PropertyChangingEventArgs(propertyName));
        }

        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            _propertyChangingEventSource ??= new PropertyChangingReentrantEventSource();
            _propertyChangingEventSource.Fire(this, args);
        }

        public void RaisePropertyChanged(string propertyName)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            _propertyChangedEventSource.Fire(this, args);
        }

        /// <summary>
        /// Temporarily suspends the invoking of subscribed actions for this instance.
        /// </summary>
        /// <returns>An <see cref="IDisposable"/> instance, indicating the lifetime of the suspension.</returns>
        public IDisposable SuspendSubscriptions()
        {
            _suspendSubscriptions = true;

            return Disposable.Create(this, o => o._suspendSubscriptions = false);
        }

        protected TRet RaiseAndSetIfChanged<TRet>(
            ref TRet backingField,
            TRet newValue,
            [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (EqualityComparer<TRet>.Default.Equals(backingField, newValue))
            {
                return newValue;
            }

            if (!_suspendSubscriptions)
            {
                RaisePropertyChanging(new PropertyChangingEventArgs(propertyName));
            }

            backingField = newValue;

            if (!_suspendSubscriptions)
            {
                RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
            }

            return newValue;
        }

        private Subscription SubscribeCore<TState>(
            Action<string, TState> handler,
            bool beforeChange,
            TState state) where TState : struct
        {
            if (beforeChange)
            {
                // ReSharper disable once ConvertToLocalFunction
                PropertyChangingEventHandler wrappedHandler = (_, e)
                    => handler(e.PropertyName!, state);

                PropertyChanging += wrappedHandler;

                return new Subscription(this, wrappedHandler);
            }
            else
            {
                // ReSharper disable once ConvertToLocalFunction
                PropertyChangedEventHandler wrappedHandler = (_, e)
                    => handler(e.PropertyName!, state);

                PropertyChanged += wrappedHandler;

                return new Subscription(this, wrappedHandler);
            }
        }

        private class PropertyChangedReentrantEventSource
            : BaseReentrantEventSource<PropertyChangedEventHandler, (object?, PropertyChangedEventArgs)>
        {
            public void Fire(object? sender, PropertyChangedEventArgs e)
            {
                FireCore((sender, e));
            }

            protected override void Invoke(PropertyChangedEventHandler @delegate, in (object?, PropertyChangedEventArgs) args)
            {
                @delegate.Invoke(args.Item1, args.Item2);
            }
        }

        private class PropertyChangingReentrantEventSource
            : BaseReentrantEventSource<PropertyChangingEventHandler, (object?, PropertyChangingEventArgs)>
        {
            public void Fire(object? sender, PropertyChangingEventArgs e)
            {
                FireCore((sender, e));
            }

            protected override void Invoke(PropertyChangingEventHandler @delegate, in (object?, PropertyChangingEventArgs) args)
            {
                @delegate.Invoke(args.Item1, args.Item2);
            }
        }

        #endregion
    }
}
