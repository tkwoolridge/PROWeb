using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Assessments;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Voters.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class DetailsView<TPersonViewModel, TAddressViewModel> : DetailsView<TPersonViewModel>
        where TPersonViewModel : SlimViewModelBase
        where TAddressViewModel : SlimViewModelBase
    {
        [Parameter]
        public AddressView<TAddressViewModel>? AddressView { get; set; }

        protected override void OnUpdateFromEligible(EligibleViewModel eligible)
        {
            base.OnUpdateFromEligible(eligible);

            if (AddressView != null && eligible.DriverLicenseAssessmentNo != null)
            {
                AssessmentViewModel assesssemnt = eligible.Adapt<AssessmentViewModel>();

                AddressView.OnUpdateAddress(assesssemnt);
            }
        }

        protected override void OnUpdateFromVoter(VoterViewModel voter)
        {
            base.OnUpdateFromVoter(voter);

            if (AddressView != null)
            {
                AssessmentViewModel assesssemnt = voter.Adapt<AssessmentViewModel>();

                AddressView.OnUpdateAddress(assesssemnt);
            }
        }

        protected DetailsView(
            Expression<Func<TPersonViewModel, int?>>? personIdPath = null,
            Expression<Func<TPersonViewModel, string?>>? titlePath = null,
            Expression<Func<TPersonViewModel, string?>>? firstNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? lastNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? middleNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? maidenNamePath = null,
            Expression<Func<TPersonViewModel, char?>>? genderPath = null,
            Expression<Func<TPersonViewModel, DateTime?>>? dateOfBirthPath = null) : 
            base
            (
                personIdPath,
                titlePath, 
                firstNamePath, 
                lastNamePath, 
                middleNamePath,
                maidenNamePath,
                genderPath,
                dateOfBirthPath
            )
        {
        }
    }
}
