namespace PROWeb.Common.Components
{
    public abstract class PRORegistryLayout<TFilter, TItem> : PROComponent where TFilter : class, new()
    {
        protected PROFilterComponent<TFilter>? FilterRef { get; set; }
        protected PROListComponent<TFilter, TItem>? ListRef { get; set; }

        public virtual async Task OnFilterAsync(TFilter filter)
        {
            if (ListRef == null)
            {
                return;
            }

            await ListRef.FilterAsync(filter);
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
