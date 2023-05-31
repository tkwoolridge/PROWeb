namespace PROWeb.Common.Components
{
    public interface IPROComponentWithState<TState>
        where TState : class
    {
        Task SetStateAsync(TState state);

        Task<TState?> GetStateAsync();

        event Action<TState>? StateChanged;

        bool PersistState { get; set; }

        void ResetState();
    }
}
