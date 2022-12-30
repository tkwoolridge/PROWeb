namespace PROWeb.Common.Components
{
    public abstract class PROListComponent<TFilter, TItem> : PROComponent where TFilter : class, new()
    {
        protected virtual string BusyMessage { get; set; } = "Loading. Please wait...";

        protected IList<TItem>? Data { get; set; }

        public virtual async Task FilterAsync(TFilter filter)
        {
            Layout?.SetBusyState(true, BusyMessage);

            Data = await OnFilterAsync(filter);

            Layout?.SetBusyState(false);

            StateHasChanged();
        }

        protected abstract Task<IList<TItem>> OnFilterAsync(TFilter filter);
    }
}
