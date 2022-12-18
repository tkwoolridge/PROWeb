using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Office.Components.Voter;

namespace PROWeb.Office.Pages
{
    public partial class VoterRegistry : PRORegistryLayout<VoterFilterViewModel, TabedVoterViewModel>
    {
        protected override string PageTitle => "Registry";

        private RenderFragment FilterTemplate;

        private bool SimpleFilterSelected { get; set; } = true;

        private bool AdvancedFilterSelected { get; set; }

        private void OnAdvancedFilterSelect(bool state)
        {
            AdvancedFilterSelected = state;

            if(AdvancedFilterSelected)
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
