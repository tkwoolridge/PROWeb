using Microsoft.AspNetCore.Components;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Layouts;
using PROWeb.Components.Person.Filters;

namespace PROWeb.Components.EligiblePolls
{
    partial class EligiblePollsDialog : PRORegistryLayout<FilterModel, EligibleViewModel>
    {
        private EligibleViewModel? _eligible;

        public bool CanConfirm { get; set; }

        [Parameter]
        public EventCallback<EligibleViewModel> Confirm { get; set; }

        [Parameter]
        public EventCallback Cancel { get; set; }

        protected bool ShowDialog { get; set; }

        public void Show()
        {
            ShowDialog = true;

            StateHasChanged();
        }

        public void Hide()
        {
            ShowDialog = false;

            StateHasChanged();
        }

        protected void OnEligibleSelected(EligibleViewModel eligible)
        {
            _eligible = eligible;

            CanConfirm = true;
        }

        protected async Task OnConfirm()
        {
            ShowDialog = false;

            await Confirm.InvokeAsync(_eligible);
        }

        protected async Task OnCancel()
        {
            ShowDialog = false;

            await Cancel.InvokeAsync();
        }
    }
}
