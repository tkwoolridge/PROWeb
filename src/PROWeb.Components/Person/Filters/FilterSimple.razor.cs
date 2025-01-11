using PROWeb.Components.Common;

namespace PROWeb.Components.Person.Filters
{
    public abstract partial class FilterSimple : PROFilterComponentWithState<FilterModel>
    {
        protected override void OnInitialized()
        {
            ResetFilter();
            base.OnInitialized();
        }

        protected override void ResetFilter()
        {
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
