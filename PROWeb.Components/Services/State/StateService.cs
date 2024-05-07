using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using PROWeb.Common.Components;

namespace PROWeb.Components.Services.State
{
    internal class StateService<TComponent, TState> : IStateService<TComponent, TState> where TComponent : IPROComponentWithState<TState>
    {
        private readonly IDictionary<string, TState> _states = new Dictionary<string, TState>();

        private readonly NavigationManager _navigation;

        public StateService(NavigationManager navigation)
        {
            _navigation = navigation;
            _navigation.LocationChanged += OnLocationChanged;
        }

        public void Clear()
        {
            _states.Clear();
        }

        public TState? GetState(string key)
        {
            _states.TryGetValue(key, out TState? state);
            return state;
        }

        public void SetState(string key, TState state)
        {
            if (!_states.TryAdd(key, state))
            {
                _states[key] = state;
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            Clear();
        }
    }
}
