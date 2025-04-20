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

            if (!firstRender)
            {
                return;
            }

            if (PersistState)
            {
                GridState<TItem>? state = GridStateService?.GetState(PersistenceKey);
                await SetStateAsync(state);
            }
        }

        protected string PersistenceKey => "/" + string.Concat(Navigation.Uri.Split("//")[1].Split("/").Skip(1));

        [Inject]
        protected IStateService<PROGridComponent<TFilter, TItem>, GridState<TItem>>? GridStateService { get; set; }

        [Inject]
        protected IStateService<PROFilterComponentWithState<TFilter>, TFilter>? FilterStateService { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; } = null!;

        protected TelerikGrid<TItem>? GridRef { get; set; }

        [Parameter]
        public bool PersistState { get; set; }

        public async Task SetStateAsync(GridState<TItem>? state)
        {
            if (GridRef is not { } grid)
            {
                return;
            }

            if (FilterStateService?.GetState(PersistenceKey) is { } filter)
            {
                await OnFilterAsync(filter);
            }

            if (state is { } gridState)
            {
                await grid.SetStateAsync(gridState);
            }
        }

        protected void OnStateChanged(GridStateEventArgs<TItem> e)
        {
            GridState<TItem>? state = e.GridState;

            GridStateService?.SetState(PersistenceKey, state);
        }

        protected void RefreshGrid(TItem current, Func<TItem, bool> predicate)
        {
            if (Data is not null &&
                Data.FirstOrDefault(predicate) is { } previous &&
                Data.IndexOf(previous) is { } index && index > -1)
            {
                Data.RemoveAt(index);
                Data.Insert(index, current);

                GridRef?.Rebind();
            }
        }

        protected void RefreshGrid(TItem current)
        {
            if (Data is not null)
            {
                Data.Insert(0, current);
                GridRef?.Rebind();
            }
        }

        protected async Task OnUpdateAsync(GridCommandEventArgs args)
        {
            Layout?.SetBusyState(true, "Updating. Please wait... ");

            await UpdateAsync(args);

            Layout?.SetBusyState(false);
        }

        protected async Task OnCreateAsync(GridCommandEventArgs args)
        {
            Layout?.SetBusyState(true, "Creating. Please wait... ");

            await CreateAsync(args);

            Layout?.SetBusyState(false);
        }

        protected virtual async Task UpdateAsync(GridCommandEventArgs args)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task CreateAsync(GridCommandEventArgs args)
        {
            await Task.CompletedTask;
        }

        public void ResetState()
        {
            FilterStateService?.Clear();
            GridStateService?.Clear();
        }
    }
}
