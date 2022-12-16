using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Assessments;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Voter;
using System.Diagnostics;

namespace PROWeb.Components.Voters.Views
{
    public partial class VoterAddressView : VoterView<IVoterAssessmentViewModel>
    {
        protected AssessmentRegistryDialog? AssessmentRegistryDialogRef { get; set; }

        protected void OnUpdate()
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            AssessmentRegistryDialogRef.Show();
        }

        private void OnAssessmentSelectionConfirm(AssessmentViewModel assessment)
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            DetailsContext.AssessmentNo = assessment.AssessmentNo;
            DetailsContext.Address1 = assessment.Address1;
            DetailsContext.Address2 = assessment.Address2;
            DetailsContext.HouseNo = assessment.HouseNo;
            DetailsContext.PostalCode = assessment.PostalCode;
            DetailsContext.ParishName = assessment.ParishName;
            DetailsContext.ConstituencyNo = assessment.ConstituencyNo;
            DetailsContext.ConstituencyName = assessment.ConstituencyName;

            StateHasChanged();
        }
    }
}
