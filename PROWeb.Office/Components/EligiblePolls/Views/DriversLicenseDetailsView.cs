using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.EligiblePolls;

namespace PROWeb.Office.Components.EligiblePolls.Views
{
    public class DriversLicenseDetailsView : EligibleDetailsView
    {
        public DriversLicenseDetailsView()
             : base(
                null,
                null,
                e => e.DriverLicenseFirstName,
                e => e.DriverLicenseLastName,
                e => e.DriverLicenseMiddleName,
                e => e.DriverLicenseGender,
                e => e.DriverLicenseDateOfBirth)
        {
        }
    }
}
