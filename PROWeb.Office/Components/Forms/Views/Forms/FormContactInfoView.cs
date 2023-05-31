using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms.Views.Forms
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
