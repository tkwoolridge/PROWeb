using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Services;
using System.Diagnostics;
using Telerik.SvgIcons;

namespace PROWeb.Components.Common
{
    public abstract class PROFilterComponent<TFilter> : 
        PROComponent
        where TFilter : SlimViewModelBase, new()
    {
        
        protected bool CanSearch { get; set; } = true;

        protected bool CanClear { get; set; } = true;

        [Parameter]
        public EventCallback<TFilter> Filtered { get; set; }

        public PROFilterComponent()
        {
            Filter = new TFilter();
        }

        public virtual TFilter Filter { get; set; }


        public void SetFilterCallBack(Func<TFilter, Task> onFiltered)
        {
            Filtered = new EventCallback<TFilter>(this, onFiltered);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Debug.Assert(Layout != null);

            Layout.BusyStateChanged -= OnBusyStateChanged;
            Layout.BusyStateChanged += OnBusyStateChanged;
        }

        private void OnBusyStateChanged(object? sender, bool state)
        {
            CanSearch = !state;
        }

        protected virtual async Task OnSearchAsync()
        {
            await Filtered.InvokeAsync(Filter);
        }

        protected virtual void OnClear()
        {
            ResetFilter();
        }

        protected virtual void ResetFilter()
        {
            Filter = new TFilter();
        }
    }
}
