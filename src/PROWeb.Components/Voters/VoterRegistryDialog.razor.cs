using Microsoft.AspNetCore.Components;
using PROWeb.Components.Layouts;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Components.Voters
{
    public partial class VoterRegistryDialog : PRORegistryLayout<FilterModel, VoterViewModel>
    {
        private VoterViewModel? _voter;

        public bool CanConfirm { get; set; }

        [Parameter]
        public EventCallback<VoterViewModel> Confirm { get; set; }

        [Parameter]
        public EventCallback Cancel { get; set; }

        protected bool ShowDialog { get; set; }

        public void Show()
        {
            ShowDialog = true;

            StateHasChanged();
        }

        private void OnSelectionChanged(VoterViewModel voter)
        {
            _voter = voter;

            CanConfirm = true;
        }

        protected async Task OnConfirm()
        {
            ShowDialog = false;

            await Confirm.InvokeAsync(_voter);
        }

        protected async Task OnCancel()
        {
            ShowDialog = false;

            await Cancel.InvokeAsync();
        }
    }
}
