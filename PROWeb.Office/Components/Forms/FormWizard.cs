using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Assessments;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Office.Components.Forms.ViewModels;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms
{
    public abstract class FormWizard : PROComponent
    {
        protected int Page { get; set; }

        protected AssessmentViewModel? SelectedAssessment { get; set; }

        protected RegistrationViewModel Registration { get; set; } = new RegistrationViewModel();

        protected FormWizardViewModel WizardModel { get; set; } =
            new FormWizardViewModel()
            {
                RegistrationActionId = 1
            };

        protected override void OnInitialized()
        {
            WizardModel.SubscribeFast(nameof(WizardModel.RegistrationActionId), OnActionChanged);
            base.OnInitialized();
        }

        protected virtual void OnActionChanged()
        {
        }

        protected virtual void OnWizardFinish()
        {
        }

        protected void ToNextPage()
        {
            Page++;

            OnPageChanged(Page);
        }

        protected void ToPreviousPage()
        {
            Page--;

            OnPageChanged(Page);
        }

        protected virtual void OnPageChanged(int page)
        {
        }

        protected void OnAssessmentSelected(object? sender, AssessmentViewModel assessment)
        {
            SelectedAssessment = assessment;
        }
    }
}
