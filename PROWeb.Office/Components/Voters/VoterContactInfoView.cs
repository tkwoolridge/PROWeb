using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.Voters
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
