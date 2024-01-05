using Microsoft.AspNetCore.Components;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Layouts;

namespace PROWeb.Components.Assessments
{
    public partial class AssessmentRegistryDialog : PRORegistryLayout<AssessmentFilterViewModel, AssessmentViewModel>
    {
        private AssessmentViewModel? _assessment;

        public bool CanConfirm { get; set; }

        protected AssessmentsGrid? BogusListRef { get; set; }

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

        protected async Task OnTabChangedAsync(int index)
        {
            if(index == 1)
            {
                await LoadBogusNumbersAsync();
            }
        }

        protected void OnAssessmentSelected(AssessmentViewModel assessment)
        {
            _assessment = assessment;

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

        private async Task LoadBogusNumbersAsync()
        {
            if (BogusListRef is not { } bogusList)
            {
                return;
            }

            await bogusList.OnFilterAsync(new AssessmentFilterViewModel
            {
                IsBogusNo = true
            });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await LoadBogusNumbersAsync();
            }
           
        }
    }
}
