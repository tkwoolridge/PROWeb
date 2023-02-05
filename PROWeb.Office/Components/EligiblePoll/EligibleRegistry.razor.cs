using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Person.Filters;
using PROWeb.Office.ViewModels.EligiblePoll;

namespace PROWeb.Office.Components.EligiblePoll
{
    public partial class EligibleRegistry : PRORegistryLayout<FilterModel, EligibleViewModel>
    {
        [Parameter]
        public bool EnableSelection { get; set; }

        [Parameter]
        public EventCallback<EligibleViewModel> EligibleSelected { get; set; }

        private async Task OnSelectionChanged(EligibleViewModel eligible)
        {
            await EligibleSelected.InvokeAsync(eligible);
        }
    }
}
