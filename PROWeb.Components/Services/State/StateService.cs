using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Components.Services.State
{
    internal class StateService<TState> : IStateService<TState> where TState : class
    {
        private readonly IDictionary<string, TState> _states = new Dictionary<string, TState>();
        private readonly IDictionary<string, IPROComponentWithState<TState>> _components = new Dictionary<string, IPROComponentWithState<TState>>();

        private readonly NavigationManager _navigation;

        public StateService(NavigationManager navigation)
        {
            _navigation = navigation;

            _navigation.LocationChanged += OnLocationChanged;
        }

        public void Clear()
        {
            if (_components.TryGetValue(GetKey(), out IPROComponentWithState<TState>? oldComponent))
            {
                oldComponent.StateChanged -= OnStateChanged;
                _components.Remove(GetKey());
            }

            _states.Remove(GetKey());
        }

        public TState? GetState()
        {
            _states.TryGetValue(GetKey(), out TState? state);
            return state;
        }

        public void RegisterComponent(IPROComponentWithState<TState> component)
        {
            if (_components.TryGetValue(GetKey(), out IPROComponentWithState<TState>? oldComponent))
            {
                oldComponent.StateChanged -= OnStateChanged;
                _components.Remove(GetKey());
            }

            component.StateChanged += OnStateChanged;
            _components.Add(GetKey(), component);
        }

        private void OnStateChanged(TState state)
        {
            if (!_states.TryAdd(GetKey(), state))
            {
                _states[GetKey()] = state;
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            Clear();
        }

        protected virtual string GetKey()
        {
            return "/" + string.Concat(_navigation.Uri.Split("//")[1].Split("/").Skip(1));
        }
    }
}
