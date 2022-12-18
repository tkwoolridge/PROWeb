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

            Context.AssessmentNo = assessment.AssessmentNo;
            Context.Address1 = assessment.Address1;
            Context.Address2 = assessment.Address2;
            Context.HouseNo = assessment.HouseNo;
            Context.PostalCode = assessment.PostalCode;
            Context.ParishName = assessment.ParishName;
            Context.ConstituencyNo = assessment.ConstituencyNo;
            Context.ConstituencyName = assessment.ConstituencyName;

            StateHasChanged();
        }
    }
}
