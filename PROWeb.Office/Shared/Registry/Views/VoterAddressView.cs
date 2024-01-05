using PROWeb.Components.Assessments;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterAddressView : AddressView<VoterViewModel>
    {
        public VoterAddressView() : base(
            v => v.AssessmentNo,
            v => v.Address1,
            v => v.HouseNo,
            v => v.Address2,
            v => v.PostalCode,
            v => v.ParishName,
            v => v.ConstituencyNo,
            v => v.ConstituencyName,
            v => v.AssessmentLongitude,
            v => v.AssessmentLatitude,
            v => v.IsAssessmentBogus
            )
        {

        }
    }
}
