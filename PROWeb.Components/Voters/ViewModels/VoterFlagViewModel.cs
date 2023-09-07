using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Voters.ViewModels
{
    public class VoterFlagViewModel : SlimViewModelBase
    {
        public int FlagId { get; set; }

        public string? FlagDescription { get; set; }
    }
}
