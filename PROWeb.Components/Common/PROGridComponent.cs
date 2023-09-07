using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Services.State;
using Telerik.Blazor.Components;

namespace PROWeb.Components.Common
{
    public abstract class PROGridComponent<TFilter, TItem> : 
        PROListComponent<TFilter, TItem>,  
        IPROComponentWithState<GridState<TItem>>
        where TFilter : SlimViewModelBase, new()
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if(!firstRender)
            {
                return;
            }

            if (PersistState)
            {
                GridStateService?.RegisterComponent(this);
                GridState<TItem>? state = GridStateService?.GetState();
                await SetStateAsync(state);
            }
        }

        [Inject]
        internal IStateService<GridState<TItem>>? GridStateService { get; set; }

        [Inject]
        internal IStateService<TFilter>? FilterStateService { get; set; }

        protected TelerikGrid<TItem>? GridRef { get; set; }

        [Parameter]
        public bool PersistState { get; set; }

        public event Action<GridState<TItem>>? StateChanged;

        public async Task SetStateAsync(GridState<TItem>? state)
        {
            if (GridRef is not { } grid)
            {
                return;
            }

            if(FilterStateService?.GetState() is { } filter)
            {
                await OnFilterAsync(filter);
            }

            if (state is { } gridState)
            {
                await grid.SetStateAsync(gridState);
            }
        }

        public async Task<GridState<TItem>?> GetStateAsync()
        {
            GridState<TItem>? state = null;

            if (GridRef is { } grid)
            {
                state = GridRef.GetState();
            }

            return await Task.FromResult(state);
        }

        protected void OnStateChanged(GridStateEventArgs<TItem> e)
        {
            GridState<TItem>? state = e.GridState;

            StateChanged?.Invoke(state);
        }

        public void ResetState()
        {
            FilterStateService?.Clear();
            GridStateService?.Clear();
        }
    }
}
