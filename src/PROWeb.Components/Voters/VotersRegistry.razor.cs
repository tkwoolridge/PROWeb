using Microsoft.AspNetCore.Components;
using PROWeb.Components.Layouts;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Components.Voters
{
    public partial class VotersRegistry : PRORegistryLayout<FilterModel, VoterViewModel>
    {
        [Parameter]
        public bool EnableSelection { get; set; } = false;

        [Parameter]
        public EventCallback<VoterViewModel> VoterSelected { get; set; }

        private async Task OnSelectionChanged(VoterViewModel voter)
        {
            await VoterSelected.InvokeAsync(voter);
        }
    }
}
