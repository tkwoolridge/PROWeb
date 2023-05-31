using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Filters;
using PROWeb.Office.ViewModels.EligiblePolls;

namespace PROWeb.Office.Components.EligiblePolls
{
    public partial class EligiblePoll : PRORegistryLayout<FilterModel, EligibleViewModel>
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
