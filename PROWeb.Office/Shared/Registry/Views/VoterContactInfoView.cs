using PROWeb.Components.Person;
using PROWeb.Office.Shared.Registry.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterContactInfoView : ContactInfoView<ListVoterViewModel>
    {
        public VoterContactInfoView() : base(
                v => v.Email,
                v => v.ContactPhone,
                v => v.PhoneHome,
                v => v.PhoneWork,
                v => v.PhoneMobile,
                v => v.DriverLicense,
                v => v.Comment
            )
        {
        }
    }
}
