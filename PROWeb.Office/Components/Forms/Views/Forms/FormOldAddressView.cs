using PROWeb.Components.Assessments;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms.Views.Forms
{
    public class FormOldAddressView : AddressView<RegistrationViewModel>
    {
        public FormOldAddressView() : base(
            r => r.OldAssessmentNo,
            r => r.OldAddress1,
            r => r.OldHouseNo,
            r => r.OldAddress2,
            r => r.OldPostalCode,
            r => r.OldParishName,
            r => r.OldConstituencyNo,
            r => r.OldConstituencyName,
            r => r.OldIsBogusNo,
            r => r.OldBogusNo,
            r => r.OldBogusConstituencyNo,
            r => r.OldBogusConstituencyName
            )
        {
        }
    }
}
