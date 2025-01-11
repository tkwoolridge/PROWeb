using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components
{
    public abstract class PROPersistentComponent : PROComponent
    {
        [Parameter]
        public bool IsPersistent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (IsPersistent && firstRender && (RestoreState() || await RestoreStateAsync()))
            {
                StateHasChanged();
            }
        }

        protected virtual Task SaveStateAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task<bool> RestoreStateAsync()
        {
            return Task.FromResult(false);
        }

        protected virtual void SaveState()
        {
        }

        protected virtual bool RestoreState()
        {
            return false;
        }
    }
}
