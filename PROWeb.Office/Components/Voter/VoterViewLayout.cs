using PROWeb.Components.Extensions;
using PROWeb.Components.Layouts;
using PROWeb.Office.ViewModels.Voter;
using VoterModel = PROWeb.Data.Models.Voter;

namespace PROWeb.Office.Office.Voter
{
    public class VoterViewLayout : ViewsLayout<ListVoterViewModel>
    {


        public override void Save()
        {
            VoterModel voter = Model.MapTo<VoterModel>(Mapper);


        }
    }
}
