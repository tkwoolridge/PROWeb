using PROWeb.Common.Components;

namespace PROWeb.Components.Person.Filters
{
    public abstract partial class FilterSimple : PROFilterComponent<FilterModel>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ResetFilter();
        }

        protected override void ResetFilter()
        {
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
