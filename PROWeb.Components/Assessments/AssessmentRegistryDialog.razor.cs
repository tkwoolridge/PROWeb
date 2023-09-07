using Microsoft.AspNetCore.Components;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Layouts;

namespace PROWeb.Components.Assessments
{
    public partial class AssessmentRegistryDialog : PRORegistryLayout<AssessmentFilterViewModel, AssessmentViewModel>
    {
        private AssessmentViewModel? _assessment;

        public bool CanConfirm { get; set; }

        [Parameter]
        public EventCallback<AssessmentViewModel> Confirm { get; set; }

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

        protected void OnAssessmentSelected(AssessmentViewModel assesment)
        {
            _assessment = assesment;

            CanConfirm = true;
        }

        protected async Task OnConfirm()
        {
            ShowDialog = false;

            await Confirm.InvokeAsync(_assessment);
        }

        protected async Task OnCancel()
        {
            ShowDialog = false;

            await Cancel.InvokeAsync();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
