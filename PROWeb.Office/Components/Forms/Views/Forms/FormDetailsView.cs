using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms.Views.Forms
{
    public class FormDetailsView : DetailsView<RegistrationViewModel>
    {
        public FormDetailsView() :
            base(
                r => r.VoterId,
                r => r.Title,
                r => r.FirstName,
                r => r.LastName,
                r => r.MiddleName,
                r => r.MaidenName,
                r => r.Gender,
                r => r.DateOfBirth)
        {
        }
    }
}
