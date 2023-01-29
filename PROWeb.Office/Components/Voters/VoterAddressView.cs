using PROWeb.Components.Assessments;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.Voters
{
    public class VoterAddressView : AddressView<ListVoterViewModel>
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
            v => v.IsBogusNo,
            v => v.BogusNo,
            v => v.BogusConstituencyNo,
            v => v.BogusConstituencyName
            )
        {

        }
    }
}
