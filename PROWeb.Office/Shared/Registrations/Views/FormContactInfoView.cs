using PROWeb.Components.Person;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormContactInfoView : ContactInfoView<RegistrationViewModel>
    {
        public FormContactInfoView() : base(
                r => r.Email,
                r => r.ContactPhone,
                r => r.PhoneHome,
                r => r.PhoneWork,
                r => r.PhoneMobile,
                r => r.DriverLicense,
                r => r.Comment
            )
        {
        }
    }
}
