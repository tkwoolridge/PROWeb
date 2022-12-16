using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Voter
{
    public class VoterFlagViewModel : ViewModelBase
    {
        public int VoterFlagId { get; set; }

        public string? FlagDescription { get; set; }
    }
}
