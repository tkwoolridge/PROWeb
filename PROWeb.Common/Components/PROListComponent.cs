using Microsoft.AspNetCore.Components;
using System.Linq;

namespace PROWeb.Common.Components
{
    public abstract class PROListComponent<TFilter, TItem> : 
        PROComponent where TFilter : class, new()
    {
        private int _page;
        private IList<TItem>? _data;

        [Parameter]
        public int PageSize { get; set; } = 20;

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

        public EventHandler<IEnumerable<TItem>>? SelectionChanged { get; set; }

        protected abstract Task<IList<TItem>> GetDataAsync(TFilter filter);

        public virtual async Task OnFilterAsync(TFilter filter)
        {
            Layout?.SetBusyState(true, BusyMessage);

            Page = 1;

            Data = await GetDataAsync(filter);

            Layout?.SetBusyState(false);

            StateHasChanged();
        }

        public virtual void SetData(IList<TItem>? data, int page, List<TItem>? selectedItems = null)
        {
            Data = data;
            Page = page;

            StateHasChanged();
        }

        protected virtual void OnSelectionChanged(IEnumerable<TItem> selected)
        {
            SelectionChanged?.Invoke(this, selected);
        }
    }
}
