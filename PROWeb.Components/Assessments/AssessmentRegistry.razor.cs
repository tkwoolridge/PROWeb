using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Assessment;

namespace PROWeb.Components.Assessments
{
    public partial class AssessmentRegistry : PRORegistryLayout<AssessmentFilterViewModel, AssessmentViewModel>
    {
        [Parameter]
        public bool EnableSelection { get; set; } = false;

        [Parameter]
        public EventCallback<AssessmentViewModel> AssessmentSelected { get; set; }

        private async Task OnSelectionChanged(AssessmentViewModel assessment)
        {
            await AssessmentSelected.InvokeAsync(assessment);
        }
    }
}
