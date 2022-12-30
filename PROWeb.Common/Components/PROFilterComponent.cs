using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace PROWeb.Common.Components
{
    public abstract class PROFilterComponent<TFilter> : PROComponent
        where TFilter : class, new()
    {
        [Parameter]
        public EventCallback<TFilter> Filtered { get; set; }

        public TFilter Filter { get; set; } = new TFilter();

        protected bool CanSearch { get; set; } = true;

        protected bool CanClear { get; set; } = true;

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
