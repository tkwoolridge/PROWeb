using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterContactInfoView : ContactInfoView<VoterViewModel>
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
