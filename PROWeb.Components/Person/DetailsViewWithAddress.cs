using Microsoft.AspNetCore.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Assessments;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.EligiblePolls.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class DetailsViewWithAddress<TPersonViewModel,TAddressViewModel> : DetailsView<TPersonViewModel>
        where TPersonViewModel : SlimViewModelBase
        where TAddressViewModel : SlimViewModelBase
    {
        [Parameter]
        public AddressView<TAddressViewModel>? AddressView { get; set; }

        public override void UpdateFromEligible(EligibleViewModel eligible)
        {
            base.UpdateFromEligible(eligible);

            if (AddressView != null && eligible.DriverLicenseAssessmentNo != null)
            {
                AssessmentViewModel assesssemnt = new AssessmentViewModel
                {
                    AssessmentNo = eligible.DriverLicenseAssessmentNo.Value,
                    HouseNo = eligible.DriverLicenseHouseNo,
                    Address1 = eligible.DriverLicenseAddress1,
                    Address2 = eligible.DriverLicenseAddress2,
                    ParishName = eligible.DriverLicenseParishName,
                    PostalCode = eligible.DriverLicensePostalCode,
                    ConstituencyNo = eligible.DriverLicenseConstituencyNo!.Value,
                    ConstituencyName = eligible.DriverLicenseConstituencyName
                };

                AddressView.UpdateAddress(assesssemnt);
            }
        }

        protected DetailsViewWithAddress(
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
