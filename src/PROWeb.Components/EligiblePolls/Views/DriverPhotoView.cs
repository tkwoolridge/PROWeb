using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Person;
namespace PROWeb.Components.EligiblePolls.Views
{
    public class DriverPhotoView : PhotosView<EligibleVoterViewModel>
    {
        public DriverPhotoView() :
            base(
                v => v.TCDPhoto,
                v => v.PROPhoto)
        {
            ShowPROPhoto = false;
        }
    }
}
