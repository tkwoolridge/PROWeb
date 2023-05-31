using Microsoft.AspNetCore.Components;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms
{
    public partial class Form2Wizard : FormWizard
    {
        public string? SecundStepLabel { get; set; }

        protected RenderFragment? SecundStepView { get; set; }

        protected VoterViewModel? SelectedVoter { get; set; }

        protected override void OnActionChanged()
        {
            SecundStepLabel = WizardModel.RegistrationActionId switch
            {
                1 => "Address Chanage",
                _ => "Name Chanage"
            };

            SecundStepView = WizardModel.RegistrationActionId switch
            {
                1 => AddressTemplate,
                _ => NameChangeTemplate
            };

            StateHasChanged();
        }

        protected override void OnPageChanged(int page)
        {
            if (page != 3)
            {
                return;
            }

            if(SelectedVoter is { } voter)
            {
                Registration = Mapper.Map<RegistrationViewModel>(voter);
            }
            
            if(SelectedAssessment is { } assessment)
            {
                Registration.Address1 = assessment.Address1;
                Registration.Address2 = assessment.Address2;
                Registration.HouseNo = assessment.HouseNo;
                Registration.ParishName = assessment.ParishName;
                Registration.PostalCode = assessment.PostalCode;
                Registration.AssessmentNo = assessment.AssessmentNo;
                Registration.ConstituencyNo = assessment.ConstituencyNo;
                Registration.ConstituencyName = assessment.ConstituencyName;
            }
        }

        protected void OnVoterSelected(object? sender, VoterViewModel voter)
        {
            SelectedVoter = voter;
        }
    }
}
