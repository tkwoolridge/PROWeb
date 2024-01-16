using Microsoft.AspNetCore.Components;
using PROWeb.Components.Layouts;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Components.Voters
{
    public partial class VoterHistoryDialog : PRORegistryLayout<VoterHistoryViewModel, VoterHistoryFieldViewModel>
    {
        protected bool ShowDialog { get; set; }

        [Parameter]
        public int? RegistryYear { get; set; }

        [Parameter]
        public int? VoterId { get; set; }

        public void Show()
        {
            ShowDialog = true;

            StateHasChanged();
        }

        protected void OnCancel()
        {
            ShowDialog = false;
        }
    }
}
