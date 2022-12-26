using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.Person.Filters
{
    public partial class PersonFilterSimple : PROFilterComponent<PersonFilterViewModel>
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
