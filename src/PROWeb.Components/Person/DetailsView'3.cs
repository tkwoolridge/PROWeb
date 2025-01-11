using Microsoft.AspNetCore.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Voters.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class DetailsView<TPersonViewModel, TAddressViewModel, TContactInfoViewModel, TFlagsViewModel, TFlagViewModel> : DetailsView<TPersonViewModel, TAddressViewModel, TContactInfoViewModel>
        where TPersonViewModel : SlimViewModelBase
        where TAddressViewModel : SlimViewModelBase
        where TContactInfoViewModel : SlimViewModelBase
        where TFlagsViewModel : SlimViewModelBase
        where TFlagViewModel : SlimViewModelBase, new()
    {
        [Parameter]
        public FlagsView<TFlagsViewModel, TFlagViewModel>? FlagsView { get; set; }

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

        protected override void OnUpdateFromVoter(VoterViewModel voter)
        {
            base.OnUpdateFromVoter(voter);

            FlagsView?.UpdateFromVoter(voter);
        }
    }
}
