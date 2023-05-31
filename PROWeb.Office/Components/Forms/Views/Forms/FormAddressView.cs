using PROWeb.Components.Assessments;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms.Views.Forms
{
    public class FormAddressView : AddressView<RegistrationViewModel>
    {
        public FormAddressView() : base(
            r => r.AssessmentNo,
            r => r.Address1,
            r => r.HouseNo,
            r => r.Address2,
            r => r.PostalCode,
            r => r.ParishName,
            r => r.ConstituencyNo,
            r => r.ConstituencyName,
            r => r.IsBogusNo,
            r => r.BogusNo,
            r => r.BogusConstituencyNo,
            r => r.BogusConstituencyName
            )
        {
        }
    }
}
