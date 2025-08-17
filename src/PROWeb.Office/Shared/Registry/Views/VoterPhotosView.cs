using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterPhotosView : PhotosView<VoterViewModel>
    {
        public VoterPhotosView() :
            base(
                v => v.TCDPhoto,
                v => v.PROPhoto)
        {
        }
    }
}
