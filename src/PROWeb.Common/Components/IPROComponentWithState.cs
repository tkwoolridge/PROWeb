namespace PROWeb.Common.Components
{
    public interface IPROComponentWithState<TState>
    {
        bool PersistState { get; set; }

        void ResetState();
    }
}
