using PROWeb.Common.Components;

namespace PROWeb.Components.Services
{
    internal interface IStateService<TState> where TState : class
    {
        TState? GetState();

        void RegisterComponent(IPROComponentWithState<TState> component);

        void Clear();
    }
}
