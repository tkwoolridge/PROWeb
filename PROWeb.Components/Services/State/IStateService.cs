using PROWeb.Common.Components;

namespace PROWeb.Components.Services.State
{
    public interface IStateService<TComponent, TState> where TComponent : IPROComponentWithState<TState>
    {
        TState? GetState(string key);

        void SetState(string key, TState state);

        void Clear();
    }
}
