using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.Voters
{
    public class VoterDetailsView : DetailsView<ListVoterViewModel>
    {
        public VoterDetailsView() : 
            base(
                v => v.VoterId, 
                v=> v.Title,
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
