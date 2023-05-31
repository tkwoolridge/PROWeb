using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.ViewModels.Assessments;
using PROWeb.Office.Components.Forms.ViewModels;

namespace PROWeb.Office.Components.Forms.Views
{
    public partial class AddressSearchView : PROView<FormWizardViewModel>
    {
        [Parameter]
        public EventHandler<AssessmentViewModel>? AssessmentSelected { get; set; }

        private void OnAssessmentSelected(AssessmentViewModel assessment)
        {
            AssessmentSelected?.Invoke(this, assessment);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
