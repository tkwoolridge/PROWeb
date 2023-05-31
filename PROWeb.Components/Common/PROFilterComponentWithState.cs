using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Services;
using Telerik.SvgIcons;

namespace PROWeb.Components.Common
{
    public class PROFilterComponentWithState<TFilter> : PROFilterComponent<TFilter>
        ,IPROComponentWithState<TFilter>
        where TFilter : SlimViewModelBase, new()
    {
        private TFilter _filter = null!;

        [Inject]
        internal IStateService<TFilter>? FilterStateService { get; set; }

        [Parameter]
        public bool PersistState { get; set; }

        public event Action<TFilter>? StateChanged;

        public override TFilter Filter
        {
            get => _filter;
            set
            {
                if (_filter is { } oldFilter)
                {
                    oldFilter.PropertyChanged -= OnFilterPropertyChanged;
                }
                _filter = value;

                if (_filter is { } newFilter)
                {
                    newFilter.PropertyChanged += OnFilterPropertyChanged;
                }
            }
        }

        private void OnFilterPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PersistState)
            {
                StateChanged?.Invoke(Filter);
            }
        }

        public async Task SetStateAsync(TFilter filter)
        {
            Filter = filter;

            await Task.CompletedTask;
        }

        public async Task<TFilter?> GetStateAsync()
        {
            return await Task.FromResult(Filter);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender && PersistState)
            {
                FilterStateService?.RegisterComponent(this);
                Filter = FilterStateService?.GetState() ?? Filter;
            }
        }

        public void ResetState()
        {
            FilterStateService?.Clear();
        }
    }
}
