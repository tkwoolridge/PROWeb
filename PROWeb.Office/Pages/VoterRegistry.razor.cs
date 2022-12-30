using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Office.ViewModels.Voter;

namespace PROWeb.Office.Pages
{
    public partial class VoterRegistry : PRORegistryLayout<PersonFilterViewModel, ListVoterViewModel>
    {
        protected override string PageTitle => "Registry";

        private RenderFragment FilterTemplate;

        private bool SimpleFilterSelected { get; set; } = true;

        private bool AdvancedFilterSelected { get; set; }

        private void OnAdvancedFilterSelect(bool state)
        {
            AdvancedFilterSelected = state;

            if (AdvancedFilterSelected)
            {
                FilterTemplate = AdvancedFilterTemplate;
            }
        }

        private void OnSimpleFilterSelect(bool state)
        {
            SimpleFilterSelected = state;

            if (SimpleFilterSelected)
            {
                FilterTemplate = SimpleFilterTemplate;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            FilterTemplate = SimpleFilterTemplate;
        }
    }
}
