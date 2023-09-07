using PROWeb.Components.Person;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormDetailsView : DetailsViewWithAddress<RegistrationViewModel, RegistrationViewModel>
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
