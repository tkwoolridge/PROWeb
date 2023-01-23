namespace PROWeb.Common.Components
{
    public abstract class PROListComponent<TFilter, TItem> : PROComponent where TFilter : class, new()
    {
        private int _page;
        private IList<TItem>? _data;

        protected virtual string BusyMessage { get; set; } = "Loading. Please wait...";

        protected int Page 
        { 
            get => _page; 
            set
            {
                if (_page != value)
                {
                    _page = value;
                    PageChanged?.Invoke(this, value);        
                }
            }
        }

        protected IList<TItem>? Data 
        { 
            get => _data; 
            set
            {
                if(_data != value)
                {
                    _data = value;
                    DataChanged?.Invoke(this, value);
                }
            } 
        }

        public EventHandler<IList<TItem>?>? DataChanged { get; set; }

        public EventHandler<int>? PageChanged { get; set; }

        public async Task FilterAsync(TFilter filter)
        {
            Layout?.SetBusyState(true, BusyMessage);

            Data = await OnFilterAsync(filter);

            Layout?.SetBusyState(false);

            StateHasChanged();
        }

        protected abstract Task<IList<TItem>> OnFilterAsync(TFilter filter);

        public virtual void SetData(IList<TItem>? data, int page)
        {
            Data = data;
            Page = page;

            StateHasChanged();
        }
    }
}
