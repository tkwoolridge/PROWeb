using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Data.Models;
using PROWeb.Office.ViewModels.EligiblePolls;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms
{
    public partial class Form1Wizard : FormWizard
    {
        public EligibleViewModel? SelectedEligible { get; set; }

        protected void OnEligibleSelected(object? sender, EligibleViewModel eligible)
        {
            SelectedEligible = eligible;
        }

        protected override void OnPageChanged(int page)
        {
            if (page != 2)
            {
                return;
            }

            if (SelectedEligible is { } eligible)
            {
                Registration = Mapper.Map<RegistrationViewModel>(eligible);
            }

            if (SelectedAssessment is { } assessment)
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
    }
}
