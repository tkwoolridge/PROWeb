using Microsoft.AspNetCore.Components;
using System.Linq;

namespace PROWeb.Common.Components
{
    public abstract class PROListComponent<TFilter, TItem> : PROComponent where TFilter : class, new()
    {
        private int _page;
        private IList<TItem>? _data;

        [Parameter]
        public int PageSize { get; set; } = 20;

        [Parameter]
        public List<TItem> SelectedItems { get; set; } = new List<TItem>();

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

        public async Task FilterAsync(TFilter filter)
        {
            SelectedItems?.Clear();

            Layout?.SetBusyState(true, BusyMessage);

            Data = await OnFilterAsync(filter);

            Layout?.SetBusyState(false);

            StateHasChanged();
        }

        protected abstract Task<IList<TItem>> OnFilterAsync(TFilter filter);

        public virtual void SetData(IList<TItem>? data, int page, List<TItem>? selectedItems = null)
        {
            Data = data;
            Page = page;
            SelectedItems = selectedItems ?? new List<TItem>();

            StateHasChanged();
        }

        protected virtual void OnSelectionChanged(IEnumerable<TItem> selected)
        {
            IEnumerable<TItem> currentPageItems = Data!.Skip(PageSize * (Page - 1)).Take(PageSize);

            if (selected.Count() == 0)
            {
                //the user de-selected all items with the header checkbox
                SelectedItems = SelectedItems.Except(currentPageItems).ToList();
            }
            else
            {
                //handle any deselected items
                var UnselectedEmployees = currentPageItems.Except(selected);
                SelectedItems = SelectedItems.Except(UnselectedEmployees).ToList();

                //add any new items if they were not selected already
                foreach (var item in selected)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }

            SelectionChanged?.Invoke(this, selected);
        }
    }
}
