using Microsoft.AspNetCore.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Voters.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public class DetailsView<TPersonViewModel, TAddressViewModel, TContactInfoViewModel> : DetailsView<TPersonViewModel, TAddressViewModel>
        where TPersonViewModel : SlimViewModelBase
        where TAddressViewModel : SlimViewModelBase
        where TContactInfoViewModel : SlimViewModelBase
    {
        [Parameter]
        public ContactInfoView<TContactInfoViewModel>? ContactInfoView { get; set; }

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

        protected override void UpdateFromVoter(VoterViewModel voter)
        {
            base.UpdateFromVoter(voter);

            ContactInfoView?.UpdateFromVoter(voter);
        }

        protected override void UpdateFromEligible(EligibleViewModel eligible)
        {
            base.UpdateFromEligible(eligible);

            ContactInfoView?.UpdateFromEligible(eligible);
        }
    }
}
