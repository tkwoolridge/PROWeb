using PROWeb.Components.Person;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormPhotosView : PhotosView<RegistrationViewModel>
    {
        public FormPhotosView()
            :base(
                 v => v.TCDPhoto,
                 v => v.PROPhoto)
        {

        }
    }
}
