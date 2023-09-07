using PROWeb.Common.Components;

namespace PROWeb.Components.Services.State
{
    internal interface IStateService<TState> where TState : class
    {
        TState? GetState();

        void RegisterComponent(IPROComponentWithState<TState> component);

        void Clear();
    }
}
