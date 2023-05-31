using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Voters
{
    public class VoterFlagViewModel : SlimViewModelBase
    {
        public int FlagId { get; set; }

        public string? FlagDescription { get; set; }
    }
}
