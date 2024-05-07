using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Services.State;

namespace PROWeb.Components.Common
{
    public class PROFilterComponentWithState<TFilter> : PROFilterComponent<TFilter>
        , IPROComponentWithState<TFilter>
        where TFilter : SlimViewModelBase, new()
    {
        private TFilter _filter = null!;

        [Parameter]
        public bool PersistState { get; set; }

        [Inject]
        protected IStateService<PROFilterComponentWithState<TFilter>, TFilter>? FilterStateService { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; } = null!;

        protected string PersistenceKey => "/" + string.Concat(Navigation.Uri.Split("//")[1].Split("/").Skip(1));

        public void ResetState()
        {
            FilterStateService?.Clear();
        }

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

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender && PersistState)
            {
                Filter = FilterStateService?.GetState(PersistenceKey) ?? Filter;
            }
        }

        private void OnFilterPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PersistState)
            {
                FilterStateService?.SetState(PersistenceKey, Filter);
            }
        }
    }
}
