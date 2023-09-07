using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class Form2View : PROComponent
    {
        protected VoterViewModel? SelectedVoter { get; set; }

        protected AssessmentViewModel? SelectedAssessment { get; set; }

        [CascadingParameter]
        public RegistrationViewModel? Model { get; set; } = new RegistrationViewModel();

        //protected override void OnPageChanged(int page)
        //{
        //    if (page != 3)
        //    {
        //        return;
        //    }

        //    if(SelectedVoter is { } voter)
        //    {
        //        Registration = Mapper.Map<RegistrationViewModel>(voter);
        //    }

        //    if(SelectedAssessment is { } assessment)
        //    {
        //        Registration.Address1 = assessment.Address1;
        //        Registration.Address2 = assessment.Address2;
        //        Registration.HouseNo = assessment.HouseNo;
        //        Registration.ParishName = assessment.ParishName;
        //        Registration.PostalCode = assessment.PostalCode;
        //        Registration.AssessmentNo = assessment.AssessmentNo;
        //        Registration.ConstituencyNo = assessment.ConstituencyNo;
        //        Registration.ConstituencyName = assessment.ConstituencyName;
        //    }
        //}

        protected void OnVoterSelected(object? sender, VoterViewModel voter)
        {
            SelectedVoter = voter;
        }

        protected void OnAssessmentSelected(object? sender, AssessmentViewModel assessment)
        {
            SelectedAssessment = assessment;
        }
    }
}
