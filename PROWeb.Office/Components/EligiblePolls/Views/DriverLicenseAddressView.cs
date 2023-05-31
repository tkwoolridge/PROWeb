using PROWeb.Components.Assessments;
using PROWeb.Office.ViewModels.EligiblePolls;

namespace PROWeb.Office.Components.EligiblePolls.Views
{
    public class DriverLicenseAddressView : AddressView<EligibleViewModel>
    {
        public DriverLicenseAddressView() 
            : base(
                e => e.DriverLicenseAssessmentNo,
                e => e.DriverLicenseAddress1,
                e => e.DriverLicenseHouseNo,
                e => e.DriverLicenseAddress2,
                e => e.DriverLicensePostalCode,
                e => e.DriverLicenseParishName,
                e => e.DriverLicenseConstituencyNo,
                e => e.DriverLicenseConstituencyName)
        { 
        }
    }
}
