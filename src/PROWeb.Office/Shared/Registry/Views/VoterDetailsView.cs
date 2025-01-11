using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterDetailsView : DetailsView<VoterViewModel>
    {
        public VoterDetailsView() :
            base(
                v => v.VoterId,
                v => v.Title,
                v => v.FirstName,
                v => v.LastName,
                v => v.MiddleName,
                v => v.MaidenName,
                v => v.Gender,
                v => v.DateOfBirth)
        {
        }
    }
}
