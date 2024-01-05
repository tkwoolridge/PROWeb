using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Assessments;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Office.Shared.Registrations.ViewModels;
using System.Diagnostics;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormDetailsView : DetailsView<RegistrationViewModel, RegistrationViewModel, RegistrationViewModel, RegistrationViewModel, VoterFlagViewModel>
    {
        [Parameter]
        public AddressView<RegistrationViewModel>? OldAddressView { get; set; }

        public FormDetailsView() :
            base(
                r => r.VoterId,
                r => r.Title,
                r => r.FirstName,
                r => r.LastName,
                r => r.MiddleName,
                r => r.MaidenName,
                r => r.Gender,
                r => r.DateOfBirth)
        {
        }

        protected override void OnUpdateFromEligible(EligibleViewModel eligible)
        {
            Debug.Assert(Model != null);

            Model.ImmigrationId = eligible.ImmigrationId;
            Model.BirthId = eligible.BirthId;

            base.OnUpdateFromEligible(eligible);

            if (OldAddressView != null && eligible.DriverLicenseAssessmentNo != null)
            {
                AssessmentViewModel assesssemnt = eligible.Adapt<AssessmentViewModel>();

                OldAddressView.OnUpdateAddress(assesssemnt);
            }
        }

        protected override void OnUpdateFromVoter(VoterViewModel voter)
        {
            base.OnUpdateFromVoter(voter);

            if (OldAddressView != null)
            {
                AssessmentViewModel assesssemnt = voter.Adapt<AssessmentViewModel>();

                OldAddressView.OnUpdateAddress(assesssemnt);
            }
        }
    }
}
