using Microsoft.AspNetCore.Components;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Layouts;
using PROWeb.Components.Person.Filters;

namespace PROWeb.Components.EligiblePolls
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
