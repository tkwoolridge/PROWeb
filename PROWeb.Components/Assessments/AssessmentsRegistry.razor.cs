using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.ViewModels.Assessments;

namespace PROWeb.Components.Assessments
{
    public partial class AssessmentsRegistry : PRORegistryLayout<AssessmentFilterViewModel, AssessmentViewModel>
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
