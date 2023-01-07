using PROWeb.Common.ViewModels;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.ViewModels.Voter
{
    public class VoterFlagViewModel : ViewModelBase, IPersonFlagViewModel
    {
        public int FlagId { get; set; }

        public string? FlagDescription { get; set; }
    }
}
