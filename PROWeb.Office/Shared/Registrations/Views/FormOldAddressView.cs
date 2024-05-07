using PROWeb.Components.Assessments;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormOldAddressView : AddressView<RegistrationViewModel>
    {
        public FormOldAddressView() : base(
            r => r.OldAssessmentNo,
            r => r.OldAddress1,
            r => r.OldHouseNo,
            r => r.OldAddress2,
            r => r.OldPostalCode,
            r => r.OldParishName,
            r => r.OldConstituencyNo,
            r => r.OldConstituencyName,
            r => r.OldAssessmentLongitude,
            r => r.OldAssessmentLatitude,
            r => r.OldIsBogus
            )
        {
        }
    }
}
