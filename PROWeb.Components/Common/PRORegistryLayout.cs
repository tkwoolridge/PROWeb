using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Services;
using Telerik.Blazor.Components;

namespace PROWeb.Components.Common
{
    public abstract class PRORegistryLayout<TFilter, TItem> : PROComponent where TFilter : SlimViewModelBase, new()
    {
        protected PROFilterComponent<TFilter>? FilterRef { get; set; }

        protected PROListComponent<TFilter, TItem>? ListRef { get; set; }

        [Parameter]
        public bool PersistState { get; set; }

        public virtual async Task OnFilterAsync(TFilter filter)
        {
            if (ListRef == null)
            {
                return;
            }

            await ListRef.OnFilterAsync(filter);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (FilterRef == null)
            {
                return;
            }
            
            FilterRef.SetFilterCallBack(OnFilterAsync);
        }
    }
}
